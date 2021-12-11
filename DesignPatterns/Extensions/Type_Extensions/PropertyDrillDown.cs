using System;
using System.Reflection;

namespace DesignPatterns.Patterns
{
    internal static class PropertyDrillDown
    {
        //this will allow drilling down into a type using a string to match to the property
        //an example is:
        // DateTime now = DateTime.Now;
        //int min = GetPropValue<int>(now, "TimeOfDay.Minutes");
        //int hrs = now.GetPropValue<int>("TimeOfDay.Hours");
        public static object GetPropValue(this object obj, string name)
        {
            foreach (string part in name.Split('.'))
            {
                if (obj == null) 
                    return null;

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);

                if (info == null) 
                    return null;

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this object obj, string name)
        {
            object result = GetPropValue(obj, name);

            if (result == null) 
                return default(T);

            // throws InvalidCastException if types are incompatible
            return (T)result;
        }
    }
}