using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSClassicTool.Managers
{
    public class FileSystemManager
    {
        public void CopyGame(string sourcePath, string destPath)
        {
            if(!System.IO.Directory.Exists(destPath))
            {
                System.IO.Directory.CreateDirectory(destPath);
            }
            foreach(string file in System.IO.Directory.GetFiles(sourcePath))
            {
                System.IO.File.Copy(file, System.IO.Path.Combine(destPath, System.IO.Path.GetFileName(file)));
            }
            foreach(string directory in System.IO.Directory.GetDirectories(sourcePath))
            {
                string dirName = System.IO.Path.GetDirectoryName(directory);
                CopyGame(directory, System.IO.Path.Combine(destPath, dirName));
            }

        }
        public void DeleteGame(long gameId)
        {
            string folder = System.IO.Path.Combine(_BasePath, gameId.ToString());
            if(System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.Delete(folder, true);
            }
        }

        private string _BasePath = "";
        public void SetBasePath(string basePath)
        {
            _BasePath = basePath;
        }
        private static FileSystemManager instance = new FileSystemManager();
        public static FileSystemManager getInstance()
        {
            return instance;
        }
        private FileSystemManager()
        {
        }
    }

}
