namespace Windows_Runtime_Examples.Example_Code
{
    public static class AsyncStreamCopy
    {
        public async static void CopyStreamsToStream(List<Stream> sources, Stream destination, Action<Stream, Stream, Exception> completed)
        {
            // set up the buffer
            byte[] buffer = new byte[0x1000];

            // create a thing to flag for when the task is done
            Action<Exception> done = e =>
            {
                if (completed == null)
                {
                    Task.FromException(e);
                }
            };

            // the actual file copy system
            AsyncCallback rc = readResult =>
            {
                try
                {
                    foreach (var source in sources)
                    {
                        int read = source.EndRead(readResult);
                        if (read > 0)
                        {
                            destination.BeginWrite(buffer, 0, read, writeResult =>
                            {
                                try
                                {
                                    // write the buffer then read the next part
                                    destination.WriteAsync(buffer, 0, read);
                                    destination.ReadAsync(buffer, 0, buffer.Length);
                                }
                                catch (Exception exc) { done(exc); }
                            }, null);
                        }
                    }
                }
                catch (Exception exc) { done(exc); }
            };
            // for each source, read the buffer
            sources.ForEach(x => x.BeginRead(buffer, 0, buffer.Length, rc, null));
        }
    }
}
