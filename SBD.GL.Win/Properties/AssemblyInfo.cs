using SBD.GL.Win;
using Scissors.Xaf.CacheWarmup.Attributes;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("SBD.GL.Win")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Software by Design Aust Pty Ltd")]
[assembly: AssemblyProduct("SBD.GL.Win")]
[assembly: AssemblyCopyright("Copyright © 2019")]
[assembly: AssemblyTrademark("JobTalk")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion("1.0.0.1")]  // do not use * in any project cause the model version info list changes. and caching will stop working.
                                      // cause dev express looks at version  use ifdef to 
[assembly: AssemblyFileVersion("1.0.0.1")]

[assembly: XafCacheWarmup(typeof(GLWindowsFormsApplication))] 