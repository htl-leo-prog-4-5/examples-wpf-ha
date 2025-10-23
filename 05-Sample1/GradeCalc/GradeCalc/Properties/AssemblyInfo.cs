using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page,
    // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page,
    // app, or any theme specific resource dictionaries)
)]

[assembly: Guid("B6D610A5-868A-4687-AAE3-19EEEE85824E")]
[assembly: AssemblyTitle("GradeCalc")]
[assembly: AssemblyDescription("Main application assembly")]

[assembly: InternalsVisibleTo("GradeCalc.Test")]