using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Searching
{
    public class SearchForKey
    {
        public async string KeyFinder(FileStream encryptedData, string[] potentialKeys)
        {
            List<string> shuffledKeys = new List<string>();
            foreach (var key in potentialKeys)
            {
                // count how many chars in the key
                char[] arrayOfChars = key.ToCharArray();
                // go through the maximum
                for (int i = 0; i < arrayOfChars.Length * arrayOfChars.Length; i++)
                {
                    // add them to the List
                    shuffledKeys.Add(Shuffle(potentialKeys[i]));
                    //discard the duplicates
                    shuffledKeys.Distinct();
                }

                for (int i = 0; i < shuffledKeys.Count(); i++)
                {
                    try
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Read))
                            {
                                using (StreamReader streamReader = new StreamReader(encryptedData))
                                {
                                    //init the byte array
                                    byte[] arr1 = new byte[] { };
                                    //write a block of bytes to the filestream
                                    encryptedData.Write(arr1);

                                    byte[] arr2 = new byte[] { };

                                    int count = 64;
                                    for (int k = 0; k < 8; k++)
                                    {
                                        for (int l = 0; l < 8; l++)
                                        {
                                            Buffer.BlockCopy(arr1, k, arr2, l, count);
                                        }
                                    }
                                    

                                }

                                byte[] array = memoryStream.ToArray();
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        public static string Shuffle(this string str)
        {
            char[] array = str.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }
    }
}
