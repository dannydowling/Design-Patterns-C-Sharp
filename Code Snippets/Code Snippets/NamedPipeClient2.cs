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
        public static StreamString ConnectToNamedPipe(this string processName, string key)
        {
            var pipeClient =
                new NamedPipeClientStream(processName, "Named_Pipe_Function_Extension",
                    PipeDirection.InOut, PipeOptions.None,
                    TokenImpersonationLevel.Identification);

            Console.WriteLine("Connecting to function server...\n");
            pipeClient.Connect();

            var functionStream = new StreamString(pipeClient);
            // Validate the server's signature string.
            if (string.Equals(functionStream.ReadString(), key.Trim()))
            {
                // The client security token is sent with the first write.
                // Send the name of the file whose contents are returned
                // by the server.
                string tokenStore = string.Format("{0},\\Identifying_Token\\Token.txt", Environment.CurrentDirectory);
                functionStream.WriteString(tokenStore);

                // Print the file to the screen.
                //Console.Write(ss.ReadString());
              
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
