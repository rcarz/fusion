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
using System.Diagnostics;
using System.Linq;
using System.Text;

using Fusion.Framework;

namespace fuse
{
    /// <summary>
    /// Synchronises the ports tree with a remote host.
    /// </summary>
    class SyncAction : IAction
    {
        private Options _options;

        /// <summary>
        /// Initialises the sync action.
        /// </summary>
        public SyncAction() { }

        /// <summary>
        /// Executes this action.
        /// </summary>
        /// <param name="pkgmgr">package manager instance</param>
        public void Execute(IPackageManager pkgmgr)
        {
            Configuration cfg = Configuration.LoadSeries();
            Security.DemandNTAdmin();

            if (cfg.PortDir == null || !cfg.PortDir.Exists)
                throw new Exception("Ports directory is not defined or doesn't exist.");

            string cygpath = "/cygdrive/" +
                cfg.PortDir.FullName.ToLower().Substring(0, 1) +
                cfg.PortDir.FullName.Substring(2).Replace("\\", "/");

            if (cfg.RsyncMirrors.Length == 0)
                throw new Exception("No rsync mirrors have been defined.");

            for (int i = 0; i < cfg.RsyncMirrors.Length; i++) {
                Uri mirror = cfg.RsyncMirrors[i];
                Console.WriteLine("\n>>> Starting rsync with {0}...\n", mirror.ToString());

                ProcessStartInfo psi = new ProcessStartInfo() {
                    FileName = Configuration.BinDir.FullName + @"\cygwin\bin\rsync.exe",
                    Arguments = String.Join(" ", new string[] { "-rltv", "--delete", mirror.ToString(), cygpath }),
                    UseShellExecute = false
                };

                Process cp = Process.Start(psi);
                cp.WaitForExit();

                if (cp.ExitCode == 0)
                    break;
            }
        }

        /// <summary>
        /// Command options structure.
        /// </summary>
        public Options Options
        {
            get { return _options; }
            set { _options = value; }
        }
    }
}
