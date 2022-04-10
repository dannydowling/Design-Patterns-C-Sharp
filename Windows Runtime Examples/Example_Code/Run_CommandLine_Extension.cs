using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Runtime_Examples.Example_Code
{
    public static class Run_CommandLine_Extension
    {
        public static string[] unfilteredData { get; set; }

        public static Action Run_CommandLine(this List<string> filepaths, string command)
        {
            using (Process process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "cmd.exe");
                process.StartInfo.RedirectStandardInput = true;              

                process.OutputDataReceived += ProcessOutputDataHandler;
                process.ErrorDataReceived += ProcessErrorDataHandler;

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                Action p = () =>
                {
                    Parallel.ForEach(filepaths, (file) =>
                    {
                        process.StandardInput.WriteLine("{0} {1}", command, file);
                    });
                };
                return p;
            }
        }

        public static void ProcessOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            unfilteredData.Append(outLine.Data);
            Console.WriteLine(outLine.Data);
        }

        public static void ProcessErrorDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            Console.WriteLine(outLine.Data);
        }
    }
}

