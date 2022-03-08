using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Code_Snippets
{
    public static class PipeClient
    {
        public static StreamString? ConnectToNamedPipe(this string processName, string key)
        {
            var pipeClient =
                new NamedPipeClientStream(processName, "Named_Pipe_Function_Extension",
                    PipeDirection.InOut, PipeOptions.None,
                    TokenImpersonationLevel.Delegation);

            Console.WriteLine("Connecting to function server...\n");
            pipeClient.Connect();

            var functionStream = new StreamString(pipeClient);
            if (key == string.Empty)
            {
                key = string.Format("{0}", File.ReadAllText("%WINDOWS%\\System32\\SettingsEnvironment.Desktop.dll"));
                key = key.Replace("\r\n", "\n");
                key = key.GetHashCode().ToString().Trim();
            }

            // Validate the server's signature string.
            // We're using a file that should be the same on both systems if they're both up to date, as a string.
            if (string.Equals(functionStream.ReadString(), key))
            {
                // The client security token is sent with the first write.
                // Send the name of the file whose contents are returned
                // by the server.
                string tokenStore = string.Format("{0},\\Identifying_Token\\Token.txt", Environment.CurrentDirectory);
                functionStream.WriteString(tokenStore);

              
              // return the rest of the datastream to the caller
               return functionStream;
            }
            else
            {
                Console.WriteLine("Server could not be verified.");          
            }

            pipeClient.Close();
            return null;
        }

        // Defines the data protocol for reading and writing strings on our stream.
        public class StreamString
        {
            private Stream ioStream;
            private UnicodeEncoding streamEncoding;

            public StreamString(Stream ioStream)
            {
                this.ioStream = ioStream;
                streamEncoding = new UnicodeEncoding();
            }

            public string ReadString()
            {
                int len;
                len = ioStream.ReadByte() * 256;
                len += ioStream.ReadByte();
                var inBuffer = new byte[len];
                ioStream.Read(inBuffer, 0, len);
                return streamEncoding.GetString(inBuffer);
            }

            public int WriteString(string outString)
            {
                byte[] outBuffer = streamEncoding.GetBytes(outString);
                int len = outBuffer.Length;
                if (len > UInt16.MaxValue)
                {
                    len = (int)UInt16.MaxValue;
                }
                ioStream.WriteByte((byte)(len / 256));
                ioStream.WriteByte((byte)(len & 255));
                ioStream.Write(outBuffer, 0, len);
                ioStream.Flush();
                return outBuffer.Length + 2;
            }
        }
    }
}
