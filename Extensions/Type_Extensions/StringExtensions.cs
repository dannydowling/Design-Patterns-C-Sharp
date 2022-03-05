using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace DesignPatterns.Extensions.Type_Extensions
{
    public static class StringExtensions
    {
        // go through the string and construct new objects from the data being read.
        public static IEnumerable<T> SplitTo<T>(this string str, StringSplitOptions options, params char[] separator)
            where T : IConvertible
        {
            return str.Split(separator, options).Select(s => (T)Convert.ChangeType(s, typeof(T)));
        }

        // create an enum from a string.
        public static T ToEnum<T>(this string value, T defaultValue = default(T)) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type T Must of type System.Enum");
            }

            T result;
            bool isParsed = Enum.TryParse(value, true, out result);
            return isParsed ? result : defaultValue;
        }

        // if a string is empty, return null instead.
        public static string GetNullIfEmptyString(this string myValue)
        {
            if (myValue == null || myValue.Length <= 0)
            {
                return null;
            }
            myValue = myValue.Trim();
            if (myValue.Length > 0)
            {
                return myValue;
            }
            return null;
        }

        // check if a string is a number
        public static bool IsNumber(this string val)
        {

            double retNum;
            bool isNum = double.TryParse(val, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        // figure out if the string is prefixed by a string
        public static bool StartsWithIgnoreCase(this string val, string prefix)
        {
            if (val == null)
            {
                throw new ArgumentNullException("val", "val parameter is null");
            }
            if (prefix == null)
            {
                throw new ArgumentNullException("prefix", "prefix parameter is null");
            }
            if (val.Length < prefix.Length)
            {
                return false;
            }
            return val.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase);
        }

        //figure out if the string is postfixed by a string
        public static bool EndsWithIgnoreCase(this string val, string suffix)
        {
            if (val == null)
            {
                throw new ArgumentNullException("val", "val parameter is null");
            }
            if (suffix == null)
            {
                throw new ArgumentNullException("suffix", "suffix parameter is null");
            }
            if (val.Length < suffix.Length)
            {
                return false;
            }
            return val.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase);
        }

        // bulk replace 
        public static string Replace(this string s, params char[] chars)
        {
            return chars.Aggregate(s, (current, c) => current.Replace(c.ToString(CultureInfo.InvariantCulture), ""));
        }

        // bulk remove
        public static string RemoveChars(this string s, params char[] chars)
        {
            var sb = new StringBuilder(s.Length);
            foreach (char c in s.Where(c => !chars.Contains(c)))
            {
                sb.Append(c);
            }
            return sb.ToString();
        }

        // transform a string into a byte array
        public static byte[] ToBytes(this string val)
        {
            var bytes = new byte[val.Length * sizeof(char)];
            Buffer.BlockCopy(val.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        // reverse a string
        public static string Reverse(this string val)
        {
            var chars = new char[val.Length];
            for (int i = val.Length - 1, j = 0; i >= 0; --i, ++j)
            {
                chars[j] = val[i];
            }
            val = new String(chars);
            return val;
        }

        // count the number of occurences
        public static int CountOccurrences(this string val, string stringToMatch)
        {
            return Regex.Matches(val, stringToMatch, RegexOptions.IgnoreCase).Count;
        }

        // alternation of expression
        public static void ReplacementWithAlternation(this string val, string stringToMatch, string alternate1, string alternate2)
        {

            var occurences = Regex.Matches(val, stringToMatch, RegexOptions.IgnoreCase);

            for (int i = 0; i < occurences.Count; i++)
            {
                if (i / 2 == 0)
                {
                    var alternationMatch1 = Regex.Matches(val, stringToMatch, RegexOptions.IgnoreCase);
                    for (int a = 0; a < alternationMatch1.Count / 2; a *= 2)
                    {
                        Regex.Replace(val, stringToMatch, alternate1);
                    }
                }
                else
                {
                    var alternationMatch2 = Regex.Matches(val, stringToMatch, RegexOptions.IgnoreCase);
                    for (int b = 1; b < alternationMatch2.Count / 2; b *= 2)
                    {
                        Regex.Replace(val, stringToMatch, alternate2);
                    }
                }
            }
        }

        // check for unicode
        public static bool IsAlphaNumeric(this string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return false;
            }
            return val.Trim().Replace(" ", "").All(Char.IsLetterOrDigit);
        }

        // change slash direction
        public static string ReverseSlash(this string val, int direction)
        {
            switch (direction)
            {
                case 0:
                    return val.Replace(@"/", @"\");
                case 1:
                    return val.Replace(@"\", @"/");
                default:
                    return val;
            }
        }

        // check an IP Address
        public static bool IsValidIPv4(this string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return false;
            }
            return Regex.Match(val,
                @"(?:^|\s)([a-z]{3,6}(?=://))?(://)?((?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.(?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.(?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.(?:25[0-5]|2[0-4]\d|[01]?\d\d?))(?::(\d{2,5}))?(?:\s|$)")
                .Success;
        }
        public static bool IsValidIPv6(this string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return false;
            }
            return Regex.Match(val,
                @"(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))")
                .Success;
        }

        public static bool IsValidIP(this string val)
        {
            bool isValid = false;
            
            if (!IsValidIPv4(val))
            { return isValid = IsValidIPv6(val); }
            else
            { return isValid = true; }           
        }

        // figure out the size of converting to another encoding
        public static int GetByteSize(this string val, Encoding encoding)
        {
            if (val == null)
            {
                throw new ArgumentNullException("val");
            }
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }
            return encoding.GetByteCount(val);
        }

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
