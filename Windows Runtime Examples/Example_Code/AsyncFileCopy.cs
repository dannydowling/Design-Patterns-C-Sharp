namespace Windows_Runtime_Examples.Example_Code
{
    public class AsyncFileCopy
    {
        public void CopyFileAsync(List<string> sources, string destinationFile, CancellationToken cancellationToken)
        {
            Toast_Notifier.NotifyOnCompletion(Task.Factory.StartNew(() =>
            {
                var fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;
                var bufferSize = 4096;

                Parallel.For(0, sources.Count, async i =>
                {
                    using (var sourceStream =
                     new FileStream(sources[i], FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, fileOptions))


                    using (var destinationStream =
                     new FileStream(destinationFile, FileMode.CreateNew, FileAccess.Write, FileShare.None, bufferSize, fileOptions))

                        await sourceStream.CopyToAsync(destinationStream, bufferSize, cancellationToken)
                                                   .ConfigureAwait(continueOnCapturedContext: false);

                });
            }), DateTime.Now);
        }

        public void CopyFilesAsync(StreamReader Source, StreamWriter Destination)
        {
            
            Toast_Notifier.NotifyOnCompletion(Task.Factory.StartNew(async () =>
            {
                char[] buffer = new char[0x1000];
                int numRead;
                while ((numRead = await Source.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    await Destination.WriteAsync(buffer, 0, numRead);
                }
            }), DateTime.Now);
        }
    }
}
