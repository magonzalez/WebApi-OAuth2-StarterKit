using System;
using System.IO;

namespace StarterKit.Framework.Files
{
    public class TempFile : IDisposable
    {
        public TempFile()
        {
            // Create a temporary file so we can get a unique name for our file.
            FileName = Path.GetTempFileName();

            // Delete the 0 byte temporary file that was created by the previous call.
            File.Delete(FileName);
        }

        public string FileName { get; private set; }

        public void Dispose()
        {
            if(!string.IsNullOrWhiteSpace(FileName))
                File.Delete(FileName);
        }
    }
}
