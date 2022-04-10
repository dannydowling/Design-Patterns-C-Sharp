using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Snippets.Code_Snippets
{
    public static class AsyncFileCopy
    {
        public static async Task CopyFileAsync(List<string> sources, string destinationFile, CancellationToken cancellationToken)
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
        }
    }
}
