using Extensions.Type_Extensions;
using System.Security.Cryptography;
using System.Text;

namespace DesignPatterns.Extensions.Function_Extensions
{
    public static class Encrypt
    {
        //use ByRSA for small things before the stream, use ByAES for large things in stream.
        public static string ByRSA<T>(this T objectToEncrypt, string key) where T: IFormattable
        {
            CspParameters cspParameter = new CspParameters { KeyContainerName = key };
            RSACryptoServiceProvider rsaServiceProvider = new RSACryptoServiceProvider(cspParameter) { PersistKeyInCsp = true };
            byte[] bytes = rsaServiceProvider.Encrypt(objectToEncrypt.ObjectToByteArray(), true);
            return BitConverter.ToString(bytes);
        }

        public static string ByAES<T>(this T objectToEncrypt, T key) where T: IFormattable
        {
            byte[] initVector = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key.ObjectToByteArray();
                aes.IV = initVector;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(objectToEncrypt);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        } 
    }

    public static class Decrypt { 

        public static T ByRSA<T>(string key , T cypherObject) where T: IFormattable
        {
            CspParameters cspParamters = new CspParameters { KeyContainerName = key };
            RSACryptoServiceProvider rsaServiceProvider = new RSACryptoServiceProvider(cspParamters) { PersistKeyInCsp = true };
         
            byte[] decryptByteArray = cypherObject.ObjectToByteArray();
            byte[] bytes = rsaServiceProvider.Decrypt(decryptByteArray, true);
            T result = bytes.ByteArrayToObject<T>();
            return result;
        }

        public static string ByAES<T>(string key, T objectToDecrypt) where T: IFormattable
        {
            byte[] initVector = new byte[16];
            byte[] buffer = objectToDecrypt.ObjectToByteArray();

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = initVector;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}

