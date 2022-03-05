using System.Text;
using Force.Crc32;

namespace DesignPatterns.Checking
{
    internal class CyclicRedundancyCheck : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public string Hash(string input)
        {
            using (var crc = new Crc32Algorithm())
            {
                var byteArray = crc.ComputeHash(Encoding.Unicode.GetBytes(input));              
                return byteArray.Aggregate("", (current, b) => current + b.ToString());
            }
        }

        public int CalcSecureHash(string text)
        {
            //new up the CRC algorithm
            var crc32 = new Crc32Algorithm();   
            //run the algorithm
            byte[] byt = new byte[text.Length];

            // converting each character into byte 
            // and store it
            for (int i = 0; i < text.Length; i++)
            {
                byt[i] = Convert.ToByte(text[i]);
            }          

            byte[] data = (crc32.ComputeHash(byt)); 
            //cycle the data based on the max of the range that the memory can hold at each position 
            int res = data[0] + (data[1] * 256) + (data[2] * 65536) + (data[3] * 16777216);
            //return
            return res;
        }
    }
}
