using System;
using System.Collections.Generic;
using System.Data.SQLite;
public class DatabaseManager
{
    public event EventHandler GameDeleted;
    public class DiscInfo
    {
        public override string ToString()
        {
            return $"Disc {DISC_NUMBER}";
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
        string discSQL = $"DELETE FROM DISC WHERE GAME_ID = {gameId}";
        string gameSQL = $"DELETE FROM GAME WHERE GAME_ID = {gameId}";
        SQLiteCommand command = new SQLiteCommand(discSQL, conn);
        command.ExecuteNonQuery();
        command = new SQLiteCommand(gameSQL, conn);
        command.ExecuteNonQuery();
        if (GameDeleted != null)
        {
            GameDeleted(null, null);
        }
    }
    public DiscInfo[] GetDiscInfo(long gameId)
    {
        var ret = new List<DiscInfo>();
        string sql = $"SELECT * FROM DISC WHERE GAME_ID = {gameId}";
        SQLiteCommand command = new SQLiteCommand(sql, conn);
        SQLiteDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            DiscInfo gi = new DiscInfo()
            {
                GAME_ID = (long)reader["GAME_ID"],
                BASENAME = (string)reader["BASENAME"],
                DISC_NUMBER = (long)reader["DISC_NUMBER"]
            };
            ret.Add(gi);
        }

        return ret.ToArray();
    }
    public long GetNextGameId()
    {
        string sql = "SELECT GAME_ID FROM GAME ORDER BY GAME_ID DESC LIMIT 1";
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
    public void UpdateGame(long gameId, string gameName,  string publishername, long year, long nbPlayer)
    {
        string sql = $"UPDATE GAME set GAME_TITLE_STRING = '{gameName}', PUBLISHER_NAME = '{publishername}', RELEASE_YEAR = {year}, PLAYERS = {nbPlayer} where GAME_ID = {gameId}";
        SQLiteCommand command = new SQLiteCommand(sql, conn);
        command.ExecuteNonQuery();
    }
    public void AddGame(string gameName, long gameId,string publishername,int year, int nbPlayer)
    {
        string sql = $"INSERT INTO GAME (GAME_ID, GAME_TITLE_STRING, PUBLISHER_NAME, RELEASE_YEAR, PLAYERS, RATING_IMAGE, GAME_MANUAL_QR_IMAGE, LINK_GAME_ID) values ({gameId}, '{gameName}', '{publishername}', {year}, {nbPlayer}, 'CERO_A', 'QR_Code_GM','')";
        SQLiteCommand command = new SQLiteCommand(sql, conn);
        command.ExecuteNonQuery();

        string folder = System.IO.Path.Combine(_basePath, $"games\\{gameId}\\gamedata");
        int index = 1;
        foreach (string cueFile in System.IO.Directory.GetFiles(folder, "*.cue", System.IO.SearchOption.TopDirectoryOnly))
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(cueFile);

            string discsql = $"INSERT INTO DISC (GAME_ID, DISC_NUMBER, BASENAME) VALUES ({gameId}, {index}, '{fileName}')";

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
        var ret = new List<GameInfo>();
        string sql = "SELECT * FROM GAME";
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

        return ret.ToArray();
    }
    string _basePath;
    public void LoadDatabase(string path)
    {
        _basePath = path;
        var dataSource = System.IO.Path.Combine(path, "System\\Databases\\regional.db");
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
