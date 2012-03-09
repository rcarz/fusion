﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Fusion.Framework
{
    /// <summary>
    /// A simple XML configuration loader.
    /// </summary>
    public sealed class XmlConfiguration
    {
        public const string CURRENTFILE = "current";
        public const string PROFILEDIR = "profiles";

        private XmlConfiguration() { }

        /// <summary>
        /// Loads the Fusion configuration series.
        /// </summary>
        /// <param name="rootdir">the Fusion root directory</param>
        /// <returns>a configuration instance with loaded values</returns>
        public static XmlConfiguration LoadSeries(DirectoryInfo rootdir)
        {
            if (!rootdir.Exists)
                throw new DirectoryNotFoundException("PortRoot not found: " + rootdir.FullName);

            DirectoryInfo[] subdiarr = rootdir.GetDirectories();
            string[] subdirs = new string[] { "bin", "conf", "global", "tmp" };
            if (subdiarr.Select(i => i.Name).Intersect(subdirs).Count() < subdirs.Count())
                throw new IOException("Bad PortRoot structure: " + rootdir.FullName);

            XmlConfiguration cfg = new XmlConfiguration();
            FileInfo[] fiarr;

            /* these directories are required */
            cfg.ConfDir = new DirectoryInfo(rootdir + @"\conf");
            cfg.PortDir = new DirectoryInfo(rootdir + @"\global");
            cfg.TmpDir = new DirectoryInfo(rootdir + @"\tmp");

            /* set defaults for optional settings */
            cfg.AcceptKeywords = new string[] { };
            cfg.CollisionDetect = false;
            cfg.PortDirOverlays = new DirectoryInfo[] { };
            cfg.PortMirrors = new Uri[] { };

            /* Determine the current profile */
            DirectoryInfo profileroot = new DirectoryInfo(cfg.PortDir + @"\" + PROFILEDIR);
            fiarr = profileroot.GetFiles(CURRENTFILE);
            if (fiarr.Length == 0)
                throw new FileNotFoundException("No profile is selected.");
            cfg.CurrentProfile = File.ReadAllText(profileroot + @"\current").TrimEnd('\r', '\n', ' ');
            cfg.ProfileDir = new DirectoryInfo(profileroot + @"\" + cfg.CurrentProfile);
            if (String.IsNullOrWhiteSpace(cfg.CurrentProfile) || !cfg.ProfileDir.Exists)
                throw new DirectoryNotFoundException("No profile found for '" + cfg.CurrentProfile + "'.");

            /* load profile config */
            fiarr = cfg.ProfileDir.GetFiles("global.xml");
            if (fiarr.Length > 0)
                cfg.LoadSingle(fiarr[0]);

            /* load machine config */
            fiarr = cfg.ConfDir.GetFiles("local.xml");
            if (fiarr.Length > 0)
                cfg.LoadSingle(fiarr[0]);

            return cfg;
        }

        /// <summary>
        /// Read in configuration data from an XML file.
        /// </summary>
        /// <param name="file">path of the config file</param>
        public void LoadSingle(FileInfo cfgfile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(cfgfile.FullName);
            XmlElement elem;

            XmlNodeList nl = doc.SelectNodes("//Configuration/AcceptKeywords/Keyword");
            List<string> blst = new List<string>(this.AcceptKeywords);
            foreach (XmlNode n in nl)
                blst.Add(n.InnerText);
            this.AcceptKeywords = blst.Distinct().ToArray();

            elem = (XmlElement)doc.SelectSingleNode("//Configuration/CollisionDetect");
            if (elem != null && !String.IsNullOrWhiteSpace(elem.InnerText))
                this.CollisionDetect = Convert.ToBoolean(elem.InnerText);
            else
                this.CollisionDetect = true;

            nl = doc.SelectNodes("//Configuration/PortDirOverlay");
            List<DirectoryInfo> overlaylst = new List<DirectoryInfo>();
            foreach (XmlNode n in nl)
                overlaylst.Add(new DirectoryInfo(((XmlElement)n).InnerText));
            this.PortDirOverlays = overlaylst.ToArray();

            nl = doc.SelectNodes("//Configuration/PortMirror");
            List<Uri> mlst = new List<Uri>();
            foreach (XmlNode n in nl)
                mlst.Add(new Uri(((XmlElement)n).InnerText));
            this.PortMirrors = mlst.ToArray();
        }

        /// <summary>
        /// Arch keywords accepted for merge.
        /// </summary>
        public string[] AcceptKeywords { get; set; }

        /// <summary>
        /// Flag for collision detection during merge.
        /// </summary>
        public bool CollisionDetect { get; set; }

        /// <summary>
        /// The local configuration directory.
        /// </summary>
        public DirectoryInfo ConfDir { get; set; }

        /// <summary>
        /// The name of the selected profile.
        /// </summary>
        public string CurrentProfile { get; set; }

        /// <summary>
        /// Name of the default package zone.
        /// </summary>
        public string DefaultZone
        {
            get { return "default"; }
        }

        /// <summary>
        /// The distfile cache directory.
        /// </summary>
        public DirectoryInfo DistFilesDir
        {
            get { return new DirectoryInfo(this.TmpDir + @"\distfiles"); }
        }

        /// <summary>
        /// The local ports directory.
        /// </summary>
        public DirectoryInfo PortDir { get; set; }

        /// <summary>
        /// User-defined port overlay directories.
        /// </summary>
        public DirectoryInfo[] PortDirOverlays { get; set; }

        /// <summary>
        /// URIs of port mirrors.
        /// </summary>
        public Uri[] PortMirrors { get; set; }

        /// <summary>
        /// The selected profile directory.
        /// </summary>
        public DirectoryInfo ProfileDir { get; set; }

        /// <summary>
        /// The sandbox root directory.
        /// </summary>
        public DirectoryInfo TmpDir { get; set; }
    }
}
