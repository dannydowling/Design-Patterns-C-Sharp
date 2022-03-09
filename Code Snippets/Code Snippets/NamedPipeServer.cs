using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;

namespace DesignPatterns.NamedPipeServer
{
    public class PipeServer
    {
        private static int numThreads = Process.GetCurrentProcess().Threads.Count - 1;
        private static bool appRunner;
        private static string appPath;
        private static string arguments;
        private static string filename;

        public static void Main()
        {
            
            Thread[] servers = new Thread[numThreads];

            Console.WriteLine("\n*** Named pipe server stream with impersonation example ***\n");

           
            
            Console.WriteLine("Waiting for client connect...\n");
            int i;
            for (i = 0; i < numThreads; i++)
            {
                servers[i] = new Thread(ServerThread);
                servers[i].Start();
            }
            Thread.Sleep(250);
            while (i > 0)
            {
                for (int j = 0; j < numThreads; j++)
                {
                    if (servers[j] != null)
                    {
                        if (servers[j].Join(250))
                        {
                            Console.WriteLine("Server thread[{0}] finished.", servers[j].ManagedThreadId);
                            servers[j] = null;
                            i--;    // decrement the thread watch count
                        }
                    }
                }
            }
            Console.WriteLine("\nServer threads exhausted, exiting.");
        }

        private static void ServerThread(object data)
        {
            NamedPipeServerStream pipeServer =
                new NamedPipeServerStream("testpipe", PipeDirection.InOut, numThreads);

            int threadId = Thread.CurrentThread.ManagedThreadId;

            // Wait for a client to connect
            pipeServer.WaitForConnection();

            Console.WriteLine("Client connected on thread[{0}].", threadId);
            try
            {
                // Read the request from the client. Once the client has
                // written to the pipe its security token will be available.

                StreamString serverStream = new StreamString(pipeServer);

                // Verify our identity to the connected client using a
                // string that the client anticipates.
                string welcomeMessage = string.Format("{0}", File.ReadAllText("%WINDOWS%\\System32\\SettingsEnvironment.Desktop.dll"));
                welcomeMessage = welcomeMessage.Replace("\r\n", "\n");
                welcomeMessage = welcomeMessage.GetHashCode().ToString();
                serverStream.WriteString(welcomeMessage);

                Console.WriteLine("Would you like to run an app on the client or read a file?");
                Console.WriteLine("Enter App for running an App, otherwise a file read will occur");
                if (Console.ReadLine() == "App")
                {
                    appRunner = true;
                    Console.WriteLine("\n*Please enter the remote path of the app to be run: *\n");
                    appPath = Console.ReadLine();
                }
                else
                {
                    appRunner = false;
                    Console.WriteLine("\n*Please enter the remote path of the app to be run: *\n");
                    filename = Console.ReadLine();
                }

                if (appRunner == false) {
                    // Read in the contents of the file while impersonating the client.
                    ReadFileToStream fileReader = new ReadFileToStream(serverStream, filename);

                    // Display the name of the user we are impersonating.
                    Console.WriteLine("Reading file: {0} on thread[{1}] as user: {2}.",
                        filename, threadId, pipeServer.GetImpersonationUserName());
                    pipeServer.RunAsClient(fileReader.Start);
                }         

                else if (appRunner == true)
                {
                    RunAppOverStream appRunner = new RunAppOverStream(serverStream, appPath, arguments);
                    // Display the name of the user we are impersonating.
                    Console.WriteLine("Running file: {0} on thread[{1}] as user: {2}.",
                        appPath, threadId, pipeServer.GetImpersonationUserName());
                    pipeServer.RunAsClient(appRunner.Start);
                }

                else
                {
                     filename = serverStream.ReadString();
                    // Read in the contents of the file while impersonating the client.
                    ReadFileToStream fileReader = new ReadFileToStream(serverStream, filename);

                    // Display the name of the user we are impersonating.
                    Console.WriteLine("Reading file: {0} on thread[{1}] as user: {2}.",
                        filename, threadId, pipeServer.GetImpersonationUserName());
                    pipeServer.RunAsClient(fileReader.Start);
                }
            }
            // Catch the IOException that is raised if the pipe is broken
            // or disconnected.
            catch (IOException e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }
            pipeServer.Close();
        }
    }

    // Defines the data protocol for reading and writing strings into the stream
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
            // this will convert from int16 to int32
            int len = ioStream.ReadByte() * 256;
            //then increment, the readstring method is called as a stream,
            //so the caller will do the iteration.
            len += ioStream.ReadByte();
            //setup the buffer
            byte[] inBuffer = new byte[len];
            //and read it into the stream
            ioStream.Read(inBuffer, 0, len);

            return streamEncoding.GetString(inBuffer);
        }

        public int WriteString(string outString)
        {
            byte[] outBuffer = streamEncoding.GetBytes(outString);
            int len = outBuffer.Length;
            if (len > ushort.MaxValue)
            {
                // if the data in the stream gets too big,
                // we'll get rid of the beginning data and keep the most recent.
                len = 65535;
            }
            ioStream.WriteByte((byte)(len / 256));
            ioStream.WriteByte((byte)(len & 255));
            ioStream.Write(outBuffer, 0, len);
            ioStream.Flush();

            return outBuffer.Length + 2;
        }
    }

    public class RunAppOverStream
    {
        private string _appPath;
        private string _arguments;
        private StreamString _appStream;

        public RunAppOverStream(StreamString str, string appPath, string arguments)
        {
            _appStream = str;
            _appPath = appPath;
            _arguments = arguments;
        }

        public void Start()
        {

            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = _arguments;
            // Enter the executable to run, including the complete path
            start.FileName = _appPath;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                // we're going to read the standard output
                proc.BeginOutputReadLine();
                //then send that over the wire
                _appStream.WriteString(proc.StandardOutput.ReadToEnd());

                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
        }
    }


// Contains the method executed in the context of the impersonated user
public class ReadFileToStream
{
    private string _fileName;
    private StreamString _streamString;

    public ReadFileToStream(StreamString str, string filename)
    {
        _fileName = filename;
        _streamString = str;
    }

    public void Start()
    {
        string contents = File.ReadAllText(_fileName);
        _streamString.WriteString(contents);
    }
}
}
