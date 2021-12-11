using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DesignPatterns.Extensions.Function_Extensions
{
    internal static class PropertyByReflection
    {
        private static string line;

        public static dynamic find<T>(this T resourceToFind) { return findAsString(resourceToFind.ToString()); }
        private static dynamic findAsString(dynamic resourceFileName)
        {
            using (StreamReader reader = GetEmbeddedResourceStream(Assembly.GetCallingAssembly(), resourceFileName))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(resourceFileName))
                    {
                        return line;
                    }
                }
            }
            return "";
        }

        private static dynamic GetEmbeddedResourceStream(Assembly assembly, dynamic resourceFileName)
        {
            var resourceNames = assembly.DefinedTypes;
            var resourcePaths = resourceNames
                .Where(x => x.DeclaredProperties == resourceFileName)
                .ToArray();
            return assembly.GetManifestResourceStream(resourcePaths.Single().GetType().ToString());
        }
    }
}
