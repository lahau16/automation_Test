
using Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class TestExtensions
    {
        public static void LoadInstalledModules(string contentRootPath)
        {
            var modules = new List<ModuleInfo>();
            var moduleRootFolder = new DirectoryInfo(Path.Combine(contentRootPath, "Modulars"));
            if (!moduleRootFolder.Exists)
            {
                GlobalConfig.Modules = modules;
                return;
            }

            var moduleFolders = moduleRootFolder.GetDirectories();

            foreach (var moduleFolder in moduleFolders)
            {
                var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.FullName, "bin"));
                if (!binFolder.Exists)
                {
                    continue;
                }

                foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
                {
                    Assembly assembly;
                    try
                    {
                        assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                    }
                    catch (FileLoadException)
                    {
                        // Get loaded assembly
                        assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));

                        if (assembly == null)
                        {
                            throw;
                        }

                        var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                        string loadedAssemblyVersion = fvi.FileVersion;

                        fvi = FileVersionInfo.GetVersionInfo(file.FullName);
                        string tryToLoadAssemblyVersion = fvi.FileVersion;

                        // Or log the exception somewhere and don't add the module to list so that it will not be initialized
                        if (tryToLoadAssemblyVersion != loadedAssemblyVersion)
                        {
                            throw new Exception($"Cannot load {file.FullName} {tryToLoadAssemblyVersion} because {assembly.Location} {loadedAssemblyVersion} has been loaded");
                        }
                    }

                    if (assembly.FullName.StartsWith(moduleFolder.Name + ","))
                    {
                        var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

                        modules.Add(new ModuleInfo
                        {
                            Name = moduleFolder.Name,
                            Assembly = assembly,
                            Path = moduleFolder.FullName,
                            Version = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion
                        });
                    }
                }
            }
            GlobalConfig.Modules = modules;
        }
    }
}
