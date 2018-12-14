using System;
using System.Collections.Generic;

public class GameManager
{
    public event EventHandler GameDeleted;
    public class DiscInfo : IComparable<DiscInfo>
    {
        public override string ToString()
        {
            return $"Disc {DISC_NUMBER}";
        }
        public long GAME_ID;
        public long DISC_NUMBER;
        public string BASENAME;
        public int CompareTo(DiscInfo inf)
        {
            return DISC_NUMBER.CompareTo(inf.DISC_NUMBER);
        }
    }
    public class GameInfo :IComparable<GameInfo>
    {
        public static bool sortByGameId = false;
        public int CompareTo(GameInfo inf)
        {
            return GAME_TITLE_STRING.CompareTo(inf.GAME_TITLE_STRING);
        }
        public long GAME_ID;
        public string GAME_TITLE_STRING;
        public string PUBLISHER_NAME;
        public long RELEASE_YEAR;
        public long PLAYERS;
        public override string ToString()
        {
            return GAME_TITLE_STRING;
        }
        public static GameInfo FromFolder(string folder)
        {
            GameInfo ret = new GameInfo();
            try
            {
                string[] splitted = folder.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                ret.GAME_ID =long.Parse( splitted[splitted.Length-1]);
                string iniPath = GameManager.getInstance().GetGameIniPath(ret.GAME_ID);
                if(System.IO.File.Exists(iniPath))
                {
                    string[] iniLines = System.IO.File.ReadAllText(iniPath).Split(new string[] { "\n" }, StringSplitOptions.None); 
                    foreach(string line in iniLines)
                    {
                        if(line.Contains("="))
                        {
                            string[] lineSplit = line.Split(new string[] { "=" }, StringSplitOptions.None);
                            if(lineSplit[0].Trim()=="Title")
                            {
                                ret.GAME_TITLE_STRING = lineSplit[1].Trim();
                            }
                            if (lineSplit[0].Trim() == "Publisher")
                            {
                                ret.PUBLISHER_NAME = lineSplit[1].Trim();
                            }
                            try
                            {
                                if (lineSplit[0].Trim() == "Players")
                                {
                                    ret.PLAYERS = long.Parse(lineSplit[1].Trim());
                                }
                                if (lineSplit[0].Trim() == "Year")
                                {
                                    ret.RELEASE_YEAR = long.Parse(lineSplit[1].Trim());
                                }
                            }catch(Exception exc)
                            {

                            }
                        }
                    }
                }
                else
                {
                    ret.GAME_TITLE_STRING = "";
                    ret.PUBLISHER_NAME = "";
                    ret.PLAYERS = 2;
                    ret.RELEASE_YEAR = 2018;

                }
            }
            catch(Exception exc)
            {
                return null;
            }
            

            return ret;
        }

        public void SaveIni()
        {
            string path = GameManager.getInstance().GetGameIniPath(GAME_ID);
            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            System.IO.File.WriteAllText(path, getIniContent());
        }
        public string getIniContent()
        {
            /*[Game]
Discs=SLUS-00594,SLUS-00776
Title=Metal Gear Solid
Publisher=Konami
Players=1
Year=1998*/
            string ret = "[Game]\r\n";
            ret += "Discs=";
            List<string> discNames = new List<string>();
            foreach(string fname in GameManager.getInstance().GetDiscFileNames(GAME_ID))
            {
                discNames.Add(fname);
            }
            ret += string.Join(",", discNames)+"\r\n";
            ret += "Title=" + GAME_TITLE_STRING + "\r\n";
            ret += "Publisher=" + PUBLISHER_NAME + " \r\n";
            ret += "Players=" + PLAYERS.ToString() + "\r\n";
            ret += "Year=" + RELEASE_YEAR.ToString() + "\r\n";
            return ret;

        }
    }
   
    public DiscInfo[] GetDiscInfo(long gameId)
    {

        var ret = new List<DiscInfo>();
        int discNumber = 1;
       
        foreach (string fname in GameManager.getInstance().GetDiscFileNames(gameId))
        {
            DiscInfo inf = new DiscInfo();
            inf.BASENAME = fname;
            inf.DISC_NUMBER = discNumber;
            inf.GAME_ID = gameId;
            discNumber++;
            ret.Add(inf);
        }
       
        return ret.ToArray();
    }
    public long GetNextGameId()
    {
        long ret = 0;
        bool used = true;
        while(used)
        {
            ret++;
            if(System.IO.Directory.Exists(System.IO.Path.Combine(_basePath,"games\\"+ ret.ToString())))
            {
                used = true;
            }
            else
            {
                used = false;
            }
        }
       
        return ret;
    }
   
  
    public GameInfo[] ListGames()
    {
        var ret = new List<GameInfo>();
        string[] files = System.IO.Directory.GetDirectories(System.IO.Path.Combine(_basePath, "games"));
        foreach(string f in files)
        {
            GameInfo gi = GameInfo.FromFolder(f);
            if(gi != null)
            {
                ret.Add(gi);
            }
        }
        GameInfo.sortByGameId = true;
        ret.Sort();
        GameInfo.sortByGameId = false;
       /* string sql = "SELECT * FROM GAME";
        SQLiteCommand command = new SQLiteCommand(sql, conn);
        SQLiteDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            GameInfo gi = new GameInfo()
            {
                GAME_ID = (long)reader["GAME_ID"],
                GAME_TITLE_STRING = (string)reader["GAME_TITLE_STRING"],
                PUBLISHER_NAME = (string)reader["PUBLISHER_NAME"],
                RELEASE_YEAR = (long)reader["RELEASE_YEAR"],
                PLAYERS = (long)reader["PLAYERS"]
            };
            ret.Add(gi);
        }
        */
        return ret.ToArray();
    }
    string _basePath;
    public void LoadDatabase(string path)
    {
        _basePath = path;
      /*  var dataSource = System.IO.Path.Combine(path, "System\\Databases\\regional.db");
        conn = new SQLiteConnection($"Data Source={dataSource};Version=3;");
        conn.Open();
        //Check if its a proper PS Classic Database
        string sql = "SELECT NAME FROM SQLITE_MASTER WHERE TYPE = 'table'";
        SQLiteCommand command = new SQLiteCommand(sql, conn);
        SQLiteDataReader reader = command.ExecuteReader();
        System.Collections.ArrayList ar = new System.Collections.ArrayList();
        while (reader.Read())
            ar.Add(reader["name"]);
        reader.Close();
        if (!ar.Contains("DISC") || !ar.Contains("GAME"))
        {
            throw new Exception("Database is not a PS Classic Database");
        }*/


    }

    /*Singleton*/
    private static GameManager instance = new GameManager();
    public static GameManager getInstance()
    {
        return instance;
    }
    private GameManager()
    {
    }
    public class DriveInfoWrapper
    {
        public bool isValid = false;
        public System.IO.DriveInfo di;
        public string message = "";
    }
    public List<string> GetDiscFileNames(long gameId)
    {
        string folder = System.IO.Path.Combine(_basePath, $"games\\{gameId}\\gamedata");
        List<string> ret = new List<string>();
        foreach (string cueFile in System.IO.Directory.GetFiles(folder, "*.cue", System.IO.SearchOption.TopDirectoryOnly))
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(cueFile);

            ret.Add(fileName); ;
        }
        return ret;
    }
    public static List<DriveInfoWrapper> ListDrives()
    {
        List<DriveInfoWrapper> validDrives = new List<DriveInfoWrapper>();
        System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
        foreach (System.IO.DriveInfo di in drives)
        {
            try
            {
                DriveInfoWrapper diw = new DriveInfoWrapper();
                diw.di = di;

                if (di.DriveType == System.IO.DriveType.Removable)
                {
                    try
                    {
                        if (di.VolumeLabel == "SONY" && di.DriveFormat == "FAT32")
                        {
                            diw.isValid = true;

                        }
                        else
                        {
                            if (di.VolumeLabel != "SONY")
                            {
                                diw.message = "Not named SONY";
                            }
                            else
                            {
                                if (di.DriveFormat != "FAT32")
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
            catch (Exception exc)
            {

            }
        }
        return validDrives;
    }
    public string GetGamePath(long gameId)
    {
        return System.IO.Path.Combine(_basePath, "Games\\" + gameId.ToString());
    }
    public string GetBoxArtPath(long gameId, string basename)
    {
        return System.IO.Path.Combine(_basePath, "Games\\" + gameId.ToString() + "\\GameData\\" + basename.ToString() + ".png");
    }
    public string GetConfigFilePath(long gameId)
    {
        return System.IO.Path.Combine(_basePath, "Games\\" + gameId.ToString() + "\\GameData\\pcsx.cfg");
    }
    public string GetGameIniPath(long gameId)
    {
        return System.IO.Path.Combine(_basePath, "Games\\" + gameId.ToString() + "\\GameData\\Game.ini");
    }
    public void SaveScript(string newScript)
    {
        try
        {
            System.IO.File.WriteAllText(System.IO.Path.Combine(_basePath, "lolhack\\lolhack.sh"), newScript.Replace("\r\n", "\n"));
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


            return System.IO.File.ReadAllText(System.IO.Path.Combine(_basePath, "lolhack\\lolhack.sh")).Replace("\n", "\r\n");
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

        string folder = System.IO.Path.Combine(_basePath, "Games\\" + gameId.ToString());
        if (System.IO.Directory.Exists(folder))
        {
            System.IO.Directory.Delete(folder, true);
        }
    }
}
