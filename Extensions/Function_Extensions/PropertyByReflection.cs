using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DesignPatterns.Extensions.Function_Extensions
{
    internal static class PropertyByReflection
    {
        private static string line;

        public static dynamic findTypeFromProperty<T>(this T propertyToFind) { return findType(propertyToFind.ToString()); }
        private static Type findType(dynamic propertyToFind)
        {
            using (StreamReader reader = GetEmbeddedResourceStream(Assembly.GetCallingAssembly(), propertyToFind))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(propertyToFind))
                    {
                        return propertyToFind.GetType();
                    }
                }
            }
            return null;
        }

        private static dynamic GetEmbeddedResourceStream(Assembly assembly, dynamic resourceFileName)
        {
            var definedTypes = assembly.DefinedTypes;
            var definedProperties = definedTypes
                .Where(x => x.DeclaredProperties == resourceFileName)
                .ToArray();
            return assembly.GetManifestResourceStream(definedProperties.Single().GetType().ToString());
        }
    }
}
