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
    /// Options for parsing atom strings.
    /// </summary>
    public enum AtomParseOptions : int
    {
        /// <summary>
        /// Parse only the package name, and reject atom strings that have a version.
        /// </summary>
        WithoutVersion = 0,

        /// <summary>
        /// Accept atom strings with or without a version.
        /// </summary>
        VersionOptional = 1,

        /// <summary>
        /// Accepts atom string only if a version is given.
        /// </summary>
        VersionRequired = 2
    }
}
