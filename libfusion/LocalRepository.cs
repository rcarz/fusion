﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;

namespace Fusion.Framework
{
    /// <summary>
    /// A file-based collection of ports arranged by category.
    /// </summary>
    public sealed class LocalRepository : AbstractTree
    {
        private XmlConfiguration _xmlconf;
        private List<ICategory> _categories;
        private Atom[] _hdmasked;
        private Atom[] _unmasked;

        /// <summary>
        /// Initialises the ports tree from a PortDir structure.
        /// </summary>
        /// <param name="xmlconf">ports config</param>
        private LocalRepository(XmlConfiguration xmlconf)
        {
            _xmlconf = xmlconf;
            _categories = new List<ICategory>(Category.Enumerate(this));
            _hdmasked = this.GetMaskedPackages();
            _unmasked = this.GetUnmaskedPackages();
        }

        /// <summary>
        /// Reads atoms from the both the profile and local package.mask file.
        /// </summary>
        /// <returns>an array of package atoms</returns>
        public Atom[] GetMaskedPackages()
        {
            List<Atom> alst = new List<Atom>();
            string[] files = new string[] { 
                _xmlconf.ProfileDir + @"\package.mask",
                _xmlconf.ConfDir + @"\package.mask" };

            foreach (string file in files) {
                FileInfo fi = new FileInfo(file);
                if (!fi.Exists) continue;

                string[] inarr = File.ReadAllLines(fi.FullName);

                foreach (string s in inarr) {
                    try {
                        if (s.StartsWith("#")) continue;
                        alst.Add(Atom.Parse(s, AtomParseOptions.VersionRequired));
                    } catch (BadAtomException) {
                        throw new BadAtomException("Bad package atom '" + s + "' in profile package.mask.");
                    }
                }
            }

            return alst.ToArray();
        }

        /// <summary>
        /// Reads atoms and keywords from both the profile and local package.keywords file.
        /// </summary>
        /// <returns>a dictionary of package keywords</returns>
        public Dictionary<Atom, string[]> GetPackageKeywords()
        {
            Dictionary<Atom, string[]> dict = new Dictionary<Atom, string[]>();
            string[] files = new string[] { 
                _xmlconf.ProfileDir + @"\package.keywords",
                _xmlconf.ConfDir + @"\package.keywords" };

            foreach (string file in files) {
                FileInfo fi = new FileInfo(file);
                if (!fi.Exists) continue;

                string[] inarr = File.ReadAllLines(fi.FullName);

                foreach (string s in inarr) {
                    try {
                        string[] parts = s.Split(
                            new char[] { ' ', '\t' },
                            StringSplitOptions.RemoveEmptyEntries);
                        if (s.StartsWith("#") || parts.Length < 2)
                            continue;

                        Atom a = Atom.Parse(parts[0], AtomParseOptions.WithoutVersion);
                        string[] brarr = new string[parts.Length - 1];
                        Array.Copy(parts, 1, brarr, 0, parts.Length - 1);

                        dict.Add(a, brarr);
                    } catch (BadAtomException) {
                        throw new BadAtomException("Bad package atom '" + s + "' in profile package.keywords.");
                    }
                }
            }

            return dict;
        }

        /// <summary>
        /// Reads atoms from the package.unmask file.
        /// </summary>
        /// <returns>an array of package atoms</returns>
        public Atom[] GetUnmaskedPackages()
        {
            List<Atom> alst = new List<Atom>();

            FileInfo fi = new FileInfo(_xmlconf.ConfDir + @"\package.unmask");
            if (fi.Exists) {
                string[] inarr = File.ReadAllLines(fi.FullName);

                foreach (string s in inarr) {
                    try {
                        if (s.StartsWith("#")) continue;
                        alst.Add(Atom.Parse(s, AtomParseOptions.VersionRequired));
                    } catch (BadAtomException) {
                        throw new BadAtomException("Bad package atom '" + s + "' in package.unmask.");
                    }
                }
            }

            return alst.ToArray();
        }

        /// <summary>
        /// Determines if the given distribution is masked, either by the profile 
        /// package.mask file or by keyword.
        /// </summary>
        /// <param name="dist">the distribution to check</param>
        /// <returns>true if masked, false otherwise</returns>
        public override bool IsMasked(IDistribution dist)
        {
            if (this.IsHardMasked(dist))
                return true;

            bool result = _xmlconf.AcceptKeywords.Intersect(dist.Keywords).Count() == 0;
            Dictionary<Atom, string[]> kwdict = this.GetPackageKeywords();

            foreach (KeyValuePair<Atom, string[]> kvp in kwdict) {
                if (kvp.Key.Match(new Atom(dist)) && kvp.Value.Intersect(dist.Keywords).Count() > 0) {
                    result = false;
                    break;
                }
            }
            
            return result;
        }

        /// <summary>
        /// Determines if the given distribution is masked by the profile package.mask file.
        /// </summary>
        /// <param name="dist">the distribution to check</param>
        /// <returns>true if masked, false otherwise</returns>
        public override bool IsHardMasked(IDistribution dist)
        {
            bool fmasked = false;

            /* look in hard-masked packages */
            foreach (Atom a in _hdmasked) {
                if (a.Match(new Atom(dist))) {
                    fmasked = true;
                    break;
                }
            }

            /* look in unmasked packages */
            foreach (Atom a in _unmasked) {
                if (a.Match(new Atom(dist))) {
                    fmasked = false;
                    break;
                }
            }

            return fmasked;
        }

        /// <summary>
        /// Loads the given ports tree from disk.
        /// </summary>
        /// <param name="xmlconf">ports config</param>
        /// <returns>the ports tree root node</returns>
        public static LocalRepository Read(XmlConfiguration xmlconf)
        {
            return new LocalRepository(xmlconf);
        }

        /// <summary>
        /// All package categories in the tree.
        /// </summary>
        public override ReadOnlyCollection<ICategory> Categories
        {
            get { return new ReadOnlyCollection<ICategory>(_categories); }
        }

        /// <summary>
        /// This ports tree root directory.
        /// </summary>
        public DirectoryInfo PortsDirectory
        {
            get { return _xmlconf.PortDir; }
        }

        /// <summary>
        /// Ports repository identifier.
        /// </summary>
        public override string Repository
        {
            get { return this.PortsDirectory.FullName; }
        }
    }
}
