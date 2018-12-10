using System;
using System.Data.SQLite;
public class DatabaseManager
{
    public event EventHandler GameDeleted;
    public class DiscInfo
    {
        public override string ToString()
        {
            return "Disc " + DISC_NUMBER.ToString();
        }
        public long GAME_ID;
        public long DISC_NUMBER;
        public string BASENAME;
    }
    public class GameInfo
    {
        public long GAME_ID;
        public string GAME_TITLE_STRING;
        public string PUBLISHER_NAME;
        public long RELEASE_YEAR;
        public long PLAYERS;
        public override string ToString()
        {
            return GAME_TITLE_STRING;
        }

    }
    public void DeleteGame(long gameId)
    {
        string discSQL = "delete from DISC where GAME_ID = " + gameId;
        string gameSQL = "delete from GAME where GAME_ID = " + gameId;
        SQLiteCommand command = new SQLiteCommand(discSQL, conn);
        command.ExecuteNonQuery();
        command = new SQLiteCommand(gameSQL, conn);
        command.ExecuteNonQuery();
        if(GameDeleted!=null)
        {
            GameDeleted(null, null);
        }
    }
    public DiscInfo[] GetDiscInfo(long gameId)
    {
        System.Collections.ArrayList ret = new System.Collections.ArrayList();
        string sql = "SELECT * FROM DISC where GAME_ID = " + gameId;
        SQLiteCommand command = new SQLiteCommand(sql, conn);
        SQLiteDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            DiscInfo gi = new DiscInfo();
            gi.GAME_ID = (long)reader["GAME_ID"];
        
            gi.BASENAME = (string)reader["BASENAME"];
            gi.DISC_NUMBER = (long)reader["DISC_NUMBER"];
       
            ret.Add(gi);
        }

        return (DiscInfo[])ret.ToArray(typeof(DiscInfo));
    }
    public long GetNextGameId()
    {
        string sql = "select GAME_ID from Game order by Game_ID desc limit 1";
        SQLiteCommand command = new SQLiteCommand(sql, conn);
        SQLiteDataReader reader = command.ExecuteReader();

        long maxGameId = -1;
        while (reader.Read())
        {
            maxGameId = (long)reader["GAME_ID"];
        }
        maxGameId++;
        return maxGameId;
    }
    public void AddGame(string gameName, long GameId)
    {
        string sql = "insert into GAME (GAME_ID,GAME_TITLE_STRING,PUBLISHER_NAME,RELEASE_YEAR,PLAYERS,RATING_IMAGE,GAME_MANUAL_QR_IMAGE,LINK_GAME_ID) values (" + GameId + ",'" + gameName + "','-',2018,2,'CERO_A','QR_CODE_GAME','')";
        SQLiteCommand command = new SQLiteCommand(sql, conn);
        command.ExecuteNonQuery();

        string folder = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(_basePath), GameId.ToString());
        int index = 1;
        foreach(string cueFile in System.IO.Directory.GetFiles(folder,"*.cue",System.IO.SearchOption.TopDirectoryOnly))
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(cueFile);

            string discsql = "insert into DISC (GAME_ID,DISC_NUMBER,BASENAME) values (" + GameId.ToString() + "," + index.ToString() + ",'" + fileName + "')";

            SQLiteCommand discCMD = new SQLiteCommand(discsql, conn);
            discCMD.ExecuteNonQuery();

            index++;
        }
        if (GameDeleted != null)
        {
            GameDeleted(null, null);
        }

    }
    public GameInfo[] ListGames()
    {
        System.Collections.ArrayList ret = new System.Collections.ArrayList();
        string sql = "SELECT * FROM GAME";
        SQLiteCommand command = new SQLiteCommand(sql, conn);
        SQLiteDataReader reader = command.ExecuteReader();
       
        while (reader.Read())
        {
            GameInfo gi = new GameInfo();
            gi.GAME_ID = (long)reader["GAME_ID"];
            gi.GAME_TITLE_STRING = (string)reader["GAME_TITLE_STRING"];
            gi.PUBLISHER_NAME = (string)reader["PUBLISHER_NAME"];
            gi.RELEASE_YEAR = (long)reader["RELEASE_YEAR"];
            gi.PLAYERS = (long)reader["PLAYERS"];
            ret.Add(gi);
        }

        return (GameInfo[])ret.ToArray(typeof(GameInfo));
    }
    string _basePath;
    public void LoadDatabase(string path)
    {
        _basePath = path;
        conn = new SQLiteConnection("Data Source=" + path + ";Version=3;");
        conn.Open();
        //Check if its a proper PS Classic Database
        string sql = "SELECT name FROM sqlite_master WHERE type = 'table'";
        SQLiteCommand command = new SQLiteCommand(sql, conn);
        SQLiteDataReader reader = command.ExecuteReader();
        System.Collections.ArrayList ar = new System.Collections.ArrayList();
        while (reader.Read())
            ar.Add(reader["name"]);
        reader.Close();
        if (!ar.Contains("DISC") || !ar.Contains("GAME"))
        {
            throw new Exception("Database is not a PS Classic Database");
        }
        
        
    }
    private SQLiteConnection conn;
    /*Singleton*/
    private static DatabaseManager instance = new DatabaseManager();
    public static DatabaseManager getInstance()
    {
        return instance;
    }
	private DatabaseManager()
	{
	}
}
