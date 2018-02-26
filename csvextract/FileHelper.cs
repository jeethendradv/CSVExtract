using System.IO;
using System.Reflection;
using System.Text;

namespace csvextract
{
    public static class FileHelper
    {
        public static string GetFullPath(string fileName)
        {
            FileInfo info = new FileInfo(fileName);
            if (!info.Exists)
            {
                throw new FileNotFoundException();
            }
            return info.FullName;
        }

        public static string GetExecutingDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
