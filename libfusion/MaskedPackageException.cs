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

namespace Fusion.Framework
{
    /// <summary>
    /// General exception for masked packages.
    /// </summary>
    public sealed class MaskedPackageException : Exception
    {
        private string _package;

        public MaskedPackageException(string package)
            : base("Encountered a masked package while performing the requested operation.")
        {
            _package = package;
        }

        /// <summary>
        /// The name of the masked package.
        /// </summary>
        public string Package
        {
            get { return _package; }
        }
    }
}
