﻿/**
 * Fusion - package management system for Windows
 * Copyright (c) 2010 Bob Carroll
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Fusion.Framework
{
    /// <summary>
    /// A descriptor for matching package versions.
    /// </summary>
    public sealed class Atom
    {
        private string _oper;
        private string _pkg;
        private PackageVersion _ver;
        private uint _slot;

        /// <summary>
        /// Regular expression for matching the category name.
        /// </summary>
        public const string CATEGORY_NAME_FMT = "[a-z0-9]+(?:-[a-z0-9]+)?";

        /// <summary>
        /// Regular expression for matching the package name.
        /// </summary>
        public const string PACKAGE_NAME_FMT = "[a-z0-9\\+_-]+";

        /// <summary>
        /// Regular expression for matching the comparison operator.
        /// </summary>
        public const string CMP_OPERATOR_FMT = "=|!|(?:<|>)=?";

        /// <summary>
        /// Regular expression for matching the slot number.
        /// </summary>
        public const string SLOT_FMT = "(?:[1-9]|[1-9][0-9]+)";

        /// <summary>
        /// Initialises an atom from a package name.
        /// </summary>
        /// <param name="pkg">the package name</param>
        private Atom(string pkg)
            : this(null, pkg, null, 0) { }

        /// <summary>
        /// Initialises an atom.
        /// </summary>
        /// <param name="oper">the comparison operator</param>
        /// <param name="pkg">the package name</param>
        /// <param name="ver">the package version</param>
        /// <param name="slot">the slot number</param>
        private Atom(string oper, string pkg, string ver, uint slot)
        {
            Debug.Assert(
                (oper == null && ver == null) ||
                (oper != null && ver != null));

            _oper = oper;
            _pkg = pkg;
            _ver = null;
            _slot = slot;

            PackageVersion.TryParse(ver, out _ver);
        }

        /// <summary>
        /// Makes a string representation of the package name and version.
        /// </summary>
        /// <param name="pkg">package name</param>
        /// <param name="ver">package version</param>
        /// <returns>formatted package version string</returns>
        public static string FormatPackageVersion(string pkg, PackageVersion ver)
        {
            return Atom.FormatPackageVersion(pkg, ver, 0);
        }

        /// <summary>
        /// Makes a string representation of the package name and slot number.
        /// </summary>
        /// <param name="pkg">package name</param>
        /// <param name="slot">slot number</param>
        /// <returns>formatted package version string</returns>
        public static string FormatPackageVersion(string pkg, uint slot)
        {
            return Atom.FormatPackageVersion(pkg, null, slot);
        }

        /// <summary>
        /// Makes a string representation of the package name, version, and slot number.
        /// </summary>
        /// <param name="pkg">package name</param>
        /// <param name="ver">package version</param>
        /// <param name="slot">slot number</param>
        /// <returns>formatted package version string</returns>
        public static string FormatPackageVersion(string pkg, PackageVersion ver, uint slot)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(pkg);

            if (ver != null)
                sb.Append(String.Format("-{0}", ver.ToString()));

            if (slot > 0)
                sb.Append(String.Format(":{0}", slot.ToString()));

            return sb.ToString();
        }

        /// <summary>
        /// Makes an atom string from the given parameters.
        /// </summary>
        /// <param name="fullname">full package name (in the form of category/package)</param>
        /// <param name="version">version string</param>
        /// <param name="slot">slot number</param>
        /// <returns>the full package name</returns>
        public static string MakeAtomString(string fullname, string version, uint slot)
        {
            string result = !String.IsNullOrEmpty(version) ?
                String.Format("{0}-{1}", fullname, version) :
                fullname;

            if (slot > 0)
                result = String.Format("{0}:{1}", result, slot);

            return result;
        }

        /// <summary>
        /// Makes a full package name from the given category and package strings.
        /// </summary>
        /// <param name="category">category name</param>
        /// <param name="package">package name</param>
        /// <returns>the full package name</returns>
        public static string MakeFullName(string category, string package)
        {
            return String.Format("{0}/{1}", category, package);
        }

        /// <summary>
        /// Determines if the given atom matches the constraints of this atom.
        /// </summary>
        /// <param name="left">the atom to compare to</param>
        /// <returns>true on match, false otherwise</returns>
        /// <remarks>
        /// Evaluation uses the input as the left side of the equation and this
        /// instance as the right side.
        /// 
        /// Example:
        ///     "this" is >=foo/bar-123
        ///     "left" is foo/bar-124
        /// 
        ///     foo/bar-124 >= foo/bar-123
        ///    
        /// In this case the match will succeed. If "this" doesn't have a
        /// comparison operator set, then the package names are compared
        /// for equality and version is ignored. The comparison operator for
        /// "left" is always ignored.
        /// 
        /// If either atom doesn't have a version then version matching is
        /// skipped. Slots are only compared if both sides have slots but
        /// no versions.
        /// </remarks>
        public bool Match(Atom left)
        {
            if (this.IsFullName && left.IsFullName && left.PackageName != _pkg)
                return false;
            else if (left.PackagePart != this.PackagePart)
                return false;

            bool lnoverslot = (!left.HasVersion && left.Slot == 0);
            bool rnoverslot = (!this.HasVersion && _slot == 0);

            if (lnoverslot || rnoverslot || _oper == null)
                return true;
            else if (!left.HasVersion || !this.HasVersion) {
                return left.Slot == _slot;
            } else if (_oper == "=" && left.Version == _ver)
                return true;
            else if (_oper == "<" && left.Version < _ver)
                return true;
            else if (_oper == "<=" && left.Version <= _ver)
                return true;
            else if (_oper == ">" && left.Version > _ver)
                return true;
            else if (_oper == ">=" && left.Version >= _ver)
                return true;
            else if (_oper == "!" && left.Version != _ver)
                return true;

            return false;
        }

        /// <summary>
        /// Converts a string into a package atom.
        /// </summary>
        /// <param name="atom">the string to parse</param>
        /// <returns>an atom</returns>
        public static Atom Parse(string atom)
        {
            return Atom.Parse(atom, AtomParseOptions.VersionOptional);
        }

        /// <summary>
        /// Converts a string into a package atom.
        /// </summary>
        /// <param name="atom">the string to parse</param>
        /// <param name="opts">parsing options</param>
        /// <returns>an atom</returns>
        public static Atom Parse(string atom, AtomParseOptions opts)
        {
            Match m;
            uint slot = 0;
            string atomstr;

            /* full package atom with implicit equals: =(category/package)-(version):(slot) */
            atomstr = String.Format(
                "^(?:=?)({0}/{1})-({2})(?:[:]({3}))?$",
                CATEGORY_NAME_FMT,
                PACKAGE_NAME_FMT,
                PackageVersion.VERSION_FMT,
                SLOT_FMT);
            m = Regex.Match(atom.ToLower(), atomstr);
            if (opts != AtomParseOptions.WithoutVersion && m.Success) {
                UInt32.TryParse(m.Groups[3].Value, out slot);
                return new Atom("=", m.Groups[1].Value, m.Groups[2].Value, slot);
            }

            /* full package name with slot and implicit equals: =(category/package):(slot) */
            atomstr = String.Format(
                "^(?:=?)({0}/{1})[:]({2})$",
                CATEGORY_NAME_FMT,
                PACKAGE_NAME_FMT,
                SLOT_FMT);
            m = Regex.Match(atom.ToLower(), atomstr);
            if (opts != AtomParseOptions.VersionRequired && m.Success) {
                UInt32.TryParse(m.Groups[2].Value, out slot);
                return new Atom("=", m.Groups[1].Value, "", slot);
            }

            /* package short name with version and implicit equals: =(package)-(version):(slot) */
            atomstr = String.Format(
                "^(?:=?)({0})-({1})(?:[:]({2}))?$",
                PACKAGE_NAME_FMT,
                PackageVersion.VERSION_FMT,
                SLOT_FMT);
            m = Regex.Match(atom.ToLower(), atomstr);
            if (opts != AtomParseOptions.WithoutVersion && m.Success) {
                UInt32.TryParse(m.Groups[3].Value, out slot);
                return new Atom("=", m.Groups[1].Value, m.Groups[2].Value, slot);
            }

            /* package short name with slot and implicit equals: =(package):(slot) */
            atomstr = String.Format(
                "^(?:=?)({0})[:]({1})$",
                PACKAGE_NAME_FMT,
                SLOT_FMT);
            m = Regex.Match(atom.ToLower(), atomstr);
            if (opts != AtomParseOptions.VersionRequired && m.Success) {
                UInt32.TryParse(m.Groups[2].Value, out slot);
                return new Atom("=", m.Groups[1].Value, "", slot);
            }

            /* full package atom: (operator)(category/package)-(version):(slot) */
            atomstr = String.Format(
                "^({0})({1}/{2})-({3})(?:[:]({4}))?$",
                CMP_OPERATOR_FMT,
                CATEGORY_NAME_FMT,
                PACKAGE_NAME_FMT,
                PackageVersion.VERSION_FMT,
                SLOT_FMT);
            m = Regex.Match(atom.ToLower(), atomstr);
            if ((opts == AtomParseOptions.VersionOptional || opts == AtomParseOptions.VersionRequired) && m.Success) {
                UInt32.TryParse(m.Groups[5].Value, out slot);
                return new Atom(m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value, slot);
            }

            /* fully-qualified package name: (category/package) */
            m = Regex.Match(atom.ToLower(), "^(" + CATEGORY_NAME_FMT + "/" + PACKAGE_NAME_FMT + ")$");
            if ((opts == AtomParseOptions.WithoutVersion || opts == AtomParseOptions.VersionOptional) && m.Success)
                return new Atom(m.Groups[1].Value);

            /* package short name: (package) */
            m = Regex.Match(atom.ToLower(), "^(" + PACKAGE_NAME_FMT + ")$");
            if ((opts == AtomParseOptions.WithoutVersion || opts == AtomParseOptions.VersionOptional) && m.Success)
                return new Atom(m.Groups[1].Value);

            throw new BadAtomException(atom);
        }

        /// <summary>
        /// Converts the given strings into package atoms.
        /// </summary>
        /// <param name="atoms">an array of strings to parse</param>
        /// <returns>an array of atoms</returns>
        public static Atom[] ParseAll(string[] atoms)
        {
            return Atom.ParseAll(atoms, AtomParseOptions.VersionOptional);
        }

        /// <summary>
        /// Converts the given strings into package atoms.
        /// </summary>
        /// <param name="atoms">an array of strings to parse</param>
        /// <param name="opts">parsing options</param>
        /// <returns>an array of atoms</returns>
        public static Atom[] ParseAll(string[] atoms, AtomParseOptions opts)
        {
            List<Atom> results = new List<Atom>();

            foreach (string a in atoms)
                results.Add(Atom.Parse(a, opts));

            return results.ToArray();
        }

        /// <summary>
        /// Gets the string representation of this atom.
        /// </summary>
        /// <returns>an atom string</returns>
        public override string ToString()
        {
            string oper = _oper != "=" ? _oper : "";

            return _ver != null ? 
                oper + Atom.FormatPackageVersion(_pkg, _ver, _slot): 
                _pkg;
        }

        /// <summary>
        /// The category name part of this atom.
        /// </summary>
        public string CategoryPart
        {
            get
            {
                return this.IsFullName ?
                    _pkg.Substring(0, _pkg.IndexOf('/')) :
                    null;
            }
        }

        /// <summary>
        /// The comparison operator specified in this atom.
        /// </summary>
        public string Comparison
        {
            get { return _oper; }
        }

        /// <summary>
        /// Determines if the atom has a version.
        /// </summary>
        /// <returns>true if it has a version, false otherwise</returns>
        public bool HasVersion
        {
            get { return _ver != null; }
        }

        /// <summary>
        /// Flag indicates whether the package name is a fully-qualified 
        /// name or a short name.
        /// </summary>
        public bool IsFullName
        {
            get { return _pkg.Contains('/'); }
        }

        /// <summary>
        /// The package name specified in this atom.
        /// </summary>
        public string PackageName
        {
            get { return _pkg; }
        }

        /// <summary>
        /// The package name part of this atom.
        /// </summary>
        public string PackagePart
        {
            get
            {
                return this.IsFullName ?
                    _pkg.Substring(_pkg.IndexOf('/') + 1) :
                    _pkg;
            }
        }

        /// <summary>
        /// The slot number specified in this atom.
        /// </summary>
        public uint Slot
        {
            get { return _slot; }
        }

        /// <summary>
        /// The package version specified in this atom.
        /// </summary>
        public PackageVersion Version
        {
            get { return _ver; }
        }
    }
}
