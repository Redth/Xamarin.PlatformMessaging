using System.Reflection;
using Foundation;

// This attribute allows you to mark your assemblies as “safe to link”.
// When the attribute is present, the linker—if enabled—will process the assembly
// even if you’re using the “Link SDK assemblies only” option, which is the default for device builds.

[assembly: LinkerSafe]

// Information about this assembly is defined by the following attributes.
// Change them to the values specific to your project.

[assembly: AssemblyTitle("Xamarin.PlatformMessaging.iOS")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Xamarin.PlatformMessaging.iOS")]
[assembly: AssemblyCopyright("Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion("1.0.*")]

// The following attributes are used to specify the signing key for the assembly,
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Xamarin.PlatformMessaging, PublicKey=00240000048000009400000006020000002400005253413100040000010001000f3373710b4a9d" +
"4784515e4cbcf4bbde09e1bbde4c65ab5991ffdd79a3786db170336ba0b51ca7de79875670cfca" +
"a86416691158fb70cfebcec3da8448dfce38abb8297d6e9ca73db6757ec931da21f66308a13a49" +
"76a84b964c5ea945ef837c1b19a83e295038b7d535bc124f19d4ce1020a2902693e04d71abf346" +
"f1d27a97")]