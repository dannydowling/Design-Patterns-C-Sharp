using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Storage.Pickers;
using Windows.Storage;

namespace Windows_Runtime_Examples.Example_Code
{
    public partial class FilePickerForCopy : Form
    {
        string source1SelectedPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string? source2SelectedPath { get; set; }
        string? source3SelectedPath { get; set; }
        string destinationSelectedPath { get; set; }

        CancellationToken cancellationToken;


        public FilePickerForCopy()
        {
            InitializeComponent();
        }
        
        private void Source_1(object sender, EventArgs e)
        {
            var source1 = new FolderBrowserDialog();
            source1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void Source_2(object sender, EventArgs e)
        {
            var source2 = new FolderBrowserDialog();
            source2.InitialDirectory = source1SelectedPath;
        }

        private void Source_3(object sender, EventArgs e)
        {
            var source3 = new FolderBrowserDialog();
            source3.InitialDirectory = source1SelectedPath;
        }

        private void Set_Destination(object sender, EventArgs e)
        {
            // choose a folder to copy to

            var openPicker = new FolderBrowserDialog();
            openPicker.InitialDirectory = source1SelectedPath;

            desitinationFolder.Text = openPicker.SelectedPath;
        }

        private void Copy(object sender, EventArgs e)
        {
            try
            {
                List<string> source = new List<string>();
                source.Add(source1SelectedPath);
                source.Add(source2SelectedPath);
                source.Add(source3SelectedPath);


                AsyncCallback callback = new AsyncCallback(ProcessFileName);

                List<string> completedFileNames = new List<string>();  

                AsyncFileCopy asyncFileCopy = new AsyncFileCopy();

                ProcessFileDelegate processFile = ProcessFileName;
                IAsyncResult asyncResult = processFile.BeginInvoke((IAsyncResult)completedFileNames, callback, completedFileNames);
                
                    asyncFileCopy.CopyFileAsync(source, destinationSelectedPath, cancellationToken);
                    completedFileNames.Add(destinationSelectedPath);
               
                processFile.EndInvoke(asyncResult);
                
            }
            catch (ArgumentNullException ex)
            {
                errorDisplay.Text = ex.Message;          
            }
        }

        public delegate void ProcessFileDelegate(IAsyncResult result);
        
        public void ProcessFileName(IAsyncResult completedFileNameBeforeAlignment)
        {
            List<string> fileNames = new List<string>();

            string fileName = completedFileNameBeforeAlignment.ToString();
            fileNames.Add(fileName);
            try
            {
                fileNames.Run_CommandLine("contig");
            }
            // Store the exception message.
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }


        private void cancel_Click(object sender, EventArgs e)
        {
            cancellationToken = new CancellationToken();
        }
    }
}
