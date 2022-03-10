namespace Patterns.Windows_Runtime
{
    internal class ReadFileToPopup
    {
        private async void ReadText()
        {
            var filename = "MyFile.txt";
            using (StreamReader sr = new StreamReader(filename))
            {
                var text = await sr.ReadToEndAsync();
                MessageBox.Show(text, "files text");
                sr.Close();
            }
        }
    }
}
