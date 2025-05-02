using System.IO;

namespace Inspector
{
    public class DiskFilesystem : IFilesystem
    {
        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}


