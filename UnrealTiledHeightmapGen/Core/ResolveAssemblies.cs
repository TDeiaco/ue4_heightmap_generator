using System;
using System.Reflection;

namespace UnrealTiledHeightmapGen.Core
{
    public static class ResolveAssemblies
    {
        private static Assembly Domain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var resourcesSuffix = ".resources";
            try
            {
                var referencedAssemblyParts = args.Name.Split(',');
                string referencedAssemblyName = $"{referencedAssemblyParts[0]}.dll";

                var executingAssembly = Assembly.GetExecutingAssembly();
                var resourceNames = executingAssembly.GetManifestResourceNames();

                var resourceToLoad = referencedAssemblyName;

                if (resourceToLoad.EndsWith(resourcesSuffix))
                {
                    var index = resourceToLoad.LastIndexOf(resourcesSuffix, StringComparison.Ordinal);
                    if (index != -1)
                        resourceToLoad = resourceToLoad.Remove(index, resourcesSuffix.Length).Insert(index, ".dll");
                }

                foreach (var resourceName in resourceNames)
                    if (resourceName.EndsWith(resourceToLoad))
                        using (var stream = executingAssembly.GetManifestResourceStream(resourceName))
                        {
                            var assemblyData = new byte[stream.Length];
                            stream.Read(assemblyData, 0, assemblyData.Length);
                            return Assembly.Load(assemblyData);
                        }
            }
            catch (Exception)
            {
                // TODO logging is currently not possible with the logging solution chosen as the logging code is in an embedded assembly.
            }
            return null;
        }

        public static void SetupAssemblyResolveOnCurrentAppDomain()
        {
            AppDomain.CurrentDomain.AssemblyResolve += Domain_AssemblyResolve;
        }
    }
}
