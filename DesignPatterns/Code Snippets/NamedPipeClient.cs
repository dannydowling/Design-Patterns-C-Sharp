using System;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NamedPipeClient
{
    internal class NamedPipeClient
    {
        async Task<string> IssueClientRequestAsync(string serverName, string msg)
        {
            using (var pipe = new NamedPipeClientStream(serverName, "PipeName", PipeDirection.InOut, PipeOptions.Asynchronous))
            {
                pipe.Connect();
                pipe.ReadMode = PipeTransmissionMode.Message;

                //send data to the server
                Byte[] request = Encoding.UTF8.GetBytes(msg);
                await pipe.WriteAsync(request, 0, request.Length);

                //read the servers response
                Byte[] response = new byte[1024];
                int bytesRead = await pipe.ReadAsync(response, 0, response.Length);
                return Encoding.UTF8.GetString(response, 0, bytesRead);
            }
        }
    }
}
