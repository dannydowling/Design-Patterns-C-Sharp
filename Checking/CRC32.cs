using System.Runtime.Serialization;
using System.Text;
using Force.Crc32;

namespace DesignPatterns.Checking
{
    internal static class CyclicRedundancyCheck 
    {
        // A CRC check is applied to data before it's sent and then again after it's recieved at the other end.
        // It divides the data up and gets a remainder value that should be the same result for both sender and reciever.

        public static string ComputeCRC32asString<T>(this T input) where T : ISerializable 
        {
            // in this first example, we'll convert to unicode and then hash that using inbuilt crc. 
            // once we've got the bytearray which is the return of crc32, we return a string that accumulates the remainder.

            if (input != null)
            {
                using (var crc = new Crc32Algorithm())
                {
                    var byteArray = crc.ComputeHash(Encoding.Unicode.GetBytes(input.ToString().Trim().Trim()));
                    return byteArray.Aggregate("", (current, b) => current + b.ToString());
                }
            }
            return string.Empty;
        }


        // this next example is more explicit. we're going to return the remainder coalesced into memory 

        public static int ComputeCRC32asInt<T>(this T input) where T : ISerializable
        {
            if (input != null)
            {
                using (var crc = new Crc32Algorithm())
                {
                    var data = crc.ComputeHash(Encoding.Unicode.GetBytes(input.ToString().Trim()));

                    // we'll align to memory to prevent overflow from a possible hardware function.
                    int remainderAsInt = data[0] + (data[1] * 256) + (data[2] * 65536) + (data[3] * 16777216);

                    return remainderAsInt;
                }
            }
            return 0;
        }  
    }
}
