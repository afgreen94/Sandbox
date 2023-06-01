using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgPull.Core
{
    public static class Utils
    {
        public static class Disk
        {
            private const string DirectorySeperator = "/";

            public static void CreateDirectoryChain(string path, bool ignoreFinal = false)
            {
                path = GetStandardizededPath(path);

                var pathParts = path.Split(DirectorySeperator);
                var drive = pathParts[0];

                var runningPathSb = new StringBuilder();
                runningPathSb.Append(drive);

                var length = ignoreFinal ? pathParts.Length - 1 : pathParts.Length;

                for(int i = 1; i < length; i++)
                {
                    runningPathSb.Append(DirectorySeperator);
                    runningPathSb.Append(pathParts[i]);

                    var currPath = runningPathSb.ToString();

                    if(!Directory.Exists(currPath))
                        Directory.CreateDirectory(currPath);
                }
            }

            public static string GetStandardizededPath(string path) => path.Replace('\\', '/');
        }
    }
}
