﻿// <copyright file="AssemblyInfo.cs" company="Santhos.net">
// Copyright (c) Santhos.net. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("MSTest data driven test")]
[assembly: AssemblyDescription("MSTest extension for data driven tests")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Santhos")]
[assembly: AssemblyProduct("MsTestDataDrivenTest")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("fa87625c-54bf-46c2-b260-2b1d57e452ea")]

// Enable unit tests on internal stuff
[assembly: InternalsVisibleTo("MsTestDataDrivenTest.UnitTests")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("2.0.0.*")]