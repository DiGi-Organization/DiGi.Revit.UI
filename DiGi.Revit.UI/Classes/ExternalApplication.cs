using Autodesk.Revit.UI;
using System.Reflection;

namespace DiGi.Revit.UI.Classes
{
    /// <summary>
    /// Revit add-in bootstrap (<see cref="IExternalApplication"/>) that enables the DiGi assembly
    /// resolver so managed and native assemblies deployed from a version-specific Revit directory
    /// load correctly.
    /// </summary>
    public sealed class ExternalApplication : IExternalApplication
    {
        private static readonly AssemblyResolver.Classes.AssemblyResolver assemblyResolver = new();

        private readonly List<IExternalApplication> externalApplications = [];

        /// <summary>
        /// Forwards shutdown to the external applications loaded during <see cref="OnStartup"/>.
        /// </summary>
        /// <param name="application">The Revit application being shut down.</param>
        /// <returns><see cref="Result.Succeeded"/></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            foreach(IExternalApplication externalApplication in externalApplications)
            {
                externalApplication?.OnShutdown(application);
            }

            return Result.Succeeded;
        }

        /// <summary>
        /// Enables the DiGi assembly resolver with managed and native probe directories for the
        /// running Revit version.
        /// </summary>
        /// <param name="application">The Revit application being started.</param>
        /// <returns>
        /// <see cref="Result.Succeeded"/> when the resolver was enabled,
        /// <see cref="Result.Cancelled"/> when the version-specific directory is missing,
        /// <see cref="Result.Failed"/> when the Revit version cannot be determined.
        /// </returns>
        public Result OnStartup(UIControlledApplication application)
        {
            if(application?.ControlledApplication?.VersionNumber is not string versionNumber)
            {
                return Result.Failed;
            }

            string path = Assembly.GetExecutingAssembly().Location;
            string directory = Path.GetDirectoryName(path)!;

            string directory_Version = Path.Combine(directory, string.Format("Revit {0}", versionNumber));
            if(string.IsNullOrWhiteSpace(directory_Version) || !Directory.Exists(directory_Version))
            {
                return Result.Cancelled;
            }

            assemblyResolver.Enable(
              managedDirectories:
              [
                directory,
                directory_Version,
                //Path.Combine(directory, "lib")
              ],
              nativeDirectories:
              [
                Path.Combine(directory, "runtimes", "win-x64", "native"),
                Path.Combine(directory_Version, "runtimes", "win-x64", "native")
              ]
            );

            // Optional: pin a specific version if you ship it (example)
            // AssemblyResolver.AddRedirect("Newtonsoft.Json",
            //   "Newtonsoft.Json, Version=13.0.3.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed");

            List<string> names = [];

            foreach (string name in names)
            {
                string path_Temp = Path.Combine(directory_Version, name);

                IExternalApplication? externalApplication = LoadExternalApplication(path_Temp);
                if(externalApplication is null)
                {
                    continue;
                }

                externalApplication.OnStartup(application);
            }

            return Result.Succeeded;
        }

        private static IExternalApplication? LoadExternalApplication(string? path)
        {
            if(string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return null;
            }

            try
            {
                Assembly? assembly = Assembly.LoadFrom(path);
                if(assembly is not null)
                {
                    Type? type = assembly.GetTypes().FirstOrDefault(t => typeof(IExternalApplication).IsAssignableFrom(t) && !t.IsAbstract);
                    if(type is not null)
                    {
                        if(Activator.CreateInstance(type) is IExternalApplication result)
                        {
                            return result;
                        }
                    }
                }
            }
            catch
            {

            }

            return null;
        }
    }
}
