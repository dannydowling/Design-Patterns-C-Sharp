using System;
using System.Runtime.CompilerServices;

namespace DesignPatterns.Extensions
{
    static internal class Not_Null
    {
        //if you call this on a type,
        //such as:
        //string s = null;
        //s.NotNull();
        //
        //the exception will say "System.ArgumentNullException: Value cannot be null. (parameter 's')
        //showing you that it was this that was the null reference exception cause.

        public static T NotNull<T>(this T? value, [CallerArgumentExpression("value")] string paramName ="") where T : notnull
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
            return value;
        }
    }
}
