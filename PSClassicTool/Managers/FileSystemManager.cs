using System;
using System.Collections.Generic;

namespace PSClassicTool.Managers
{
    public class FileSystemManager
    {
        public class DriveInfoWrapper
        {
            public bool isValid = false;
            public System.IO.DriveInfo di;
            public string message = "";
        }
        public static List<DriveInfoWrapper> ListDrives()
        {
            List<DriveInfoWrapper> validDrives = new List<DriveInfoWrapper>();
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo di in drives)
            {
                DriveInfoWrapper diw = new DriveInfoWrapper();
                diw.di = di;

                if( di.DriveType == System.IO.DriveType.Removable)
                {
                    try
                    {
                        if (di.VolumeLabel == "SONY" && di.DriveFormat == "FAT32")
                        {
                            diw.isValid = true;
                           
                        }
                        else
                        {
                            if(di.VolumeLabel != "SONY")
                            {
                                diw.message = "Not named SONY";
                            }
                            else
                            {
                                if(di.DriveFormat != "FAT32")
                                {
                                    diw.message = "Not FAT32";
                                }
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        diw.message = exc.Message;
                    }


                }
                else
                {
                    diw.message = "Not removable";
                }
                validDrives.Add(diw);
            }
            return validDrives;
        }
        public string GetBoxArtPath(long gameId, string basename)
        {
            return System.IO.Path.Combine(_BasePath, "Games\\" + gameId.ToString() + "\\GameData\\" + basename.ToString()+".png");
        }
        public string GetConfigFilePath(long gameId)
        {
            return System.IO.Path.Combine(_BasePath, "Games\\"+gameId.ToString() + "\\GameData\\pcsx.cfg");
        }
        public void SaveScript(string newScript)
        {
            try
            {
                System.IO.File.WriteAllText(System.IO.Path.Combine(_BasePath, "lolhack\\lolhack.sh"), newScript.Replace("\r\n", "\n"));
            }
            catch (Exception exc)
            {
                Console.Write("");
            }
        }
        public string GetScript()
        {
            try
            {


                return System.IO.File.ReadAllText(System.IO.Path.Combine(_BasePath,  "lolhack\\lolhack.sh")).Replace("\n","\r\n");
            }
            catch (Exception exc)
            {
                return "";
            }
        }
        public void CopyGame(string sourcePath, string destPath)
        {
            if (!System.IO.Directory.Exists(destPath))
            {
                System.IO.Directory.CreateDirectory(destPath);
            }
            foreach (string file in System.IO.Directory.GetFiles(sourcePath))
            {
                System.IO.File.Copy(file, System.IO.Path.Combine(destPath, System.IO.Path.GetFileName(file)));
            }
            foreach (string directory in System.IO.Directory.GetDirectories(sourcePath))
            {
                string dirName = System.IO.Path.GetDirectoryName(directory);
                CopyGame(directory, System.IO.Path.Combine(destPath, dirName));
            }

        }
        public void DeleteGame(long gameId)
        {
            string folder = System.IO.Path.Combine(_BasePath,"Games\\"+ gameId.ToString());
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
