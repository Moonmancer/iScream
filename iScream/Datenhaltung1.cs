using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace iScream
{
    class Datenhaltung1 : IDatenhaltung
    {
        public Datenhaltung1()
        {
            SQL.Init();
        }

        #region Holen
        public int GetNextUser_id()
        {
            DataRowCollection rows = SQL.Select("TOP 1 User_id", "[User]", "", "User_id DESC");
            if (rows.Count == 0)
                return 1;
            else
                return Convert.ToInt32(rows[0]["User_id"]) + 1;
        }

        public int GetNextGame_id()
        {
            DataRowCollection rows = SQL.Select("TOP 1 Game_id", "Games", "", "Game_id DESC");
            if (rows.Count == 0)
                return 1;
            else
                return Convert.ToInt32(rows[0]["Game_id"]) + 1;
        }

        public User GetUser(int user_id)
        {
            DataRowCollection rows = SQL.Select("*", "[User]", "User_id = " + user_id);
            if (rows.Count == 0)
                return null;
            else
                return new User(
                    Convert.ToString(rows[0]["Firstname"]),
                    Convert.ToString(rows[0]["Lastname"]),
                    Convert.ToInt32(rows[0]["User_id"])
                );
        }

        public List<User> GetUser()
        {
            DataRowCollection rows = SQL.Select("*", "[User]");
            if (rows.Count == 0)
                return null;
            else
            {
                List<User> result = new List<User>();

                foreach (DataRow row in rows)
                    result.Add(new User(
                        Convert.ToString(row["Firstname"]),
                        Convert.ToString(row["Lastname"]),
                        Convert.ToInt32(row["User_id"])
                    ));
                return result;
            }
        }

        public Game GetGame(int game_id)
        {
            DataRowCollection rows = SQL.Select("*", "Games", "Game_id = " + game_id);
            if (rows.Count == 0)
                return null;
            else
                return new Game(
                    Convert.ToString(rows[0]["Name"]),
                    Convert.ToInt32(rows[0]["Game_id"])
                );
        }

        public List<Game> GetGame()
        {
            DataRowCollection rows = SQL.Select("*", "Games");
            if (rows.Count == 0)
                return null;
            else
            {
                List<Game> result = new List<Game>();

                foreach (DataRow row in rows)
                    result.Add(new Game(
                        Convert.ToString(row["Name"]),
                        Convert.ToInt32(row["Game_id"])
                    ));
                return result;
            }
        }
        public Link GetLink(int user_id, int game_id)
        {
            DataRowCollection rows = SQL.Select("*", "Links", "User_id = " + user_id + "AND Game_id = " + game_id);
            if (rows.Count == 0)
                return null;
            else
                return new Link(
                    Convert.ToInt32(rows[0]["User_id"]),
                    Convert.ToInt32(rows[0]["Game_id"])
                );
        }

        public Link GetLink(Link verknüpfung)
        {
            return GetLink(verknüpfung.User_id, verknüpfung.Game_id);
        }

        public List<Link> GetLink()
        {

            DataRowCollection rows = SQL.Select("*", "Links");
            if (rows.Count == 0)
                return null;
            else
            {
                List<Link> result = new List<Link>();

                foreach (DataRow row in rows)
                    result.Add(new Link(
                        Convert.ToInt32(row["User_id"]),
                        Convert.ToInt32(row["Game_id"])
                    ));
                return result;
            }
        }

        public List<Game> GetGamesOfUser(int user_id)
        {
            DataRowCollection rows = SQL.Select("Game_id", "Links", "User_id = " + user_id);
            if (rows.Count == 0)
                return null;
            else
            {
                List<Game> result = new List<Game>();

                foreach (DataRow row in rows)
                    result.Add(GetGame(Convert.ToInt32(row[0])));

                return result;
            }
        }

        public List<Game> GetGamesOfUser(User user)
        {
            return GetGamesOfUser(user.User_id);
        }

        public List<User> GetUserOfGame(int game_id)
        {
            DataRowCollection rows = SQL.Select("User_id", "Links", "Game_id = " + game_id);
            if (rows.Count == 0)
                return null;
            else
            {
                List<User> result = new List<User>();

                foreach (DataRow row in rows)
                    result.Add(GetUser(Convert.ToInt32(row[0])));

                return result;
            }
        }

        public List<User> GetUserOfGame(Game game)
        {
            return GetUserOfGame(game.Game_id);
        }
        #endregion

        #region Hinzufügen
        public bool AddUser(string vorname, string nachname, int user_id)
        {
            return AddUser(new User(vorname, nachname, user_id));
        }

        public bool AddUser(string vorname, string nachname)
        {
            return AddUser(new User(vorname, nachname, GetNextUser_id()));
        }

        public bool AddUser(User user)
        {
            if (user.User_id == -1)
                user.User_id = GetNextUser_id();

            return SQL.Insert("[User]", new string[] { "Firstname", "Lastname", "User_id" }, new object[] { user.Firstname, user.Lastname, user.User_id });
        }

        public void AddUser(List<User> user)
        {
            foreach (User cur in user)
                AddUser(cur);
        }

        public bool AddGame(string name, int game_id)
        {
            return AddGame(new Game(name, game_id));
        }

        public bool AddGame(string name)
        {
            return AddGame(new Game(name, GetNextGame_id()));
        }
        public bool AddGame(Game game)
        {
            if (game.Game_id == -1)
                game.Game_id = GetNextGame_id();

            return SQL.Insert("Games", new string[] { "Name", "Game_id" }, new object[] { game.Name, game.Game_id });
        }

        public void AddGame(List<Game> spiele)
        {
            foreach (Game cur in spiele)
                AddGame(cur);
        }

        public bool AddLink(int user_id, int game_id)
        {
            return AddLink(new Link(user_id, game_id));
        }

        public bool AddLink(Link verknüpfung)
        {
            return SQL.Insert("Links", new string[] { "User_id", "Game_id" }, new object[] { verknüpfung.User_id, verknüpfung.Game_id });
        }

        public void AddLink(List<Link> verknüpfungen)
        {
            foreach (Link cur in verknüpfungen)
                AddLink(cur);
        }
        #endregion

        #region Löschen
        public bool DeleteUser(int user_id)
        {
            if (Convert.ToInt32(SQL.Delete("[User]", "User_id = " + user_id)) > 0)
            {
                SQL.Delete("Links", "User_id = " + user_id);
                return true;
            }
            else
                return false;
        }

        public bool DeleteUser(User user)
        {
            return DeleteUser(user.User_id);
        }

        public void DeleteUser(List<User> user)
        {
            foreach (User cur in user)
                DeleteUser(cur.User_id);
        }

        public bool DeleteGame(int game_id)
        {

            if (Convert.ToInt32(SQL.Delete("Games", "Game_id = " + game_id)) > 0)
            {
                SQL.Delete("Links", "Game_id = " + game_id);
                return true;
            }
            else
                return false;
        }

        public bool DeleteGame(Game game)
        {
            return DeleteGame(game.Game_id);
        }

        public void DeleteGame(List<Game> spiele)
        {
            foreach (Game cur in spiele)
                DeleteUser(cur.Game_id);
        }

        public bool DeleteLink(int user_id, int game_id)
        {
            return Convert.ToInt32(SQL.Delete("Links", "User_id = " + user_id + " AND Game_id = " + game_id)) > 0;
        }

        public bool DeleteLink(Link verknüpfung)
        {
            return DeleteLink(verknüpfung.User_id, verknüpfung.Game_id);
        }
        #endregion

        #region Ändern
        public bool UpdateUser(int user_id, string vorname, string nachname)
        {
            if (SQL.Select("User_id", "[User]", "User_id = " + user_id).Count == 0)
                return false;

            SQL.Update("[User]", new string[] { "Firstname", "Lastname" }, new object[] { vorname, nachname }, "User_id = " + user_id);

            return true;
        }

        public bool UpdateUser(User user)
        {
            return UpdateUser(user.User_id, user.Firstname, user.Lastname);
        }


        public bool UpdateGame(int game_id, string name)
        {
            if (SQL.Select("Game_id", "Games", "Game_id = " + game_id).Count > 0)
                return false;

            SQL.Update("Games", new string[] { "Name" }, new object[] { name }, "Game_id = " + game_id);

            return true;
        }

        public bool UpdateGame(Game game)
        {
            return UpdateGame(game.Game_id, game.Name);
        }
        #endregion

        #region Suchen
        public List<User> SearchUser(string vorname, string nachname)
        {
            List<User> result = new List<User>();

            string cmdText = "SELECT * FROM User WHERE ";

            if (!String.IsNullOrEmpty(vorname))
                cmdText += "Firstname like '%" + vorname + "%'";

            if (!String.IsNullOrEmpty(nachname))
                if (!String.IsNullOrEmpty(vorname))
                    cmdText += " && Lastname like '%" + nachname + "%'";
                else
                    cmdText += "Lastname like '%" + nachname + "%'";

            DataRowCollection rows = SQL.Select(cmdText);

            foreach (DataRow row in rows)
                result.Add(new User(Convert.ToString(row["Firstname"]), Convert.ToString(row["Lastname"]), Convert.ToInt32(row["User_id"])));

            return result;
        }

        public List<Game> SearchGame(string name)
        {
            List<Game> result = new List<Game>();

            string cmdText = "SELECT * FROM Games WHERE ";
            if (!String.IsNullOrEmpty(name))
                cmdText += "Name like '%" + name + "%'";

            DataRowCollection rows = SQL.Select(cmdText);

            foreach (DataRow row in rows)
                result.Add(new Game(Convert.ToString(row["Name"]), Convert.ToInt32(row["User_id"])));

            return result;
        }
        #endregion
    }

    static class SQL
    {
        static bool WRITE_LOG = true;

        public static string DB_VERSION
        {
            get
            {
                return "1.0";
            }
        }

        #region Standardwerte
        static string defaultServer = Settings.CurrentSettings.SqlServerLocation;

        static string defaultDB = Settings.CurrentSettings.SqlDatabaseName;

        static string defaultInstance;

        static bool useWinAuth = Settings.CurrentSettings.UseWinAuth;

        static string defaultUsername = Kryptographie.Entschlüsseln(Settings.CurrentSettings.SqlServerUsername);

        static string defaultPassword = Kryptographie.Entschlüsseln(Settings.CurrentSettings.SqlServerPassword);
        #endregion

        static string CONNECTIONSTRING;

        static SqlConnection SQL_CONNECTION;

        static List<string> logLines = new List<string>();

        /// <summary>
        /// Fügt dem SQL Log den angegebenen Text hinzu
        /// </summary>
        /// <param name="text"></param>
        private static void Log(string text)
        {
            if (WRITE_LOG)
                logLines.Add(text);
        }

        /// <summary>
        /// Speichert das Log in die angegebene Datei
        /// </summary>
        /// <param name="path"></param>
        public static void SaveLogToFile(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string fileName = Path.Combine(path, @"SQL_LOG_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".txt");
            File.WriteAllLines(fileName, logLines);
            System.Diagnostics.Process.Start(fileName);
        }

        /// <summary>
        /// Initialisiert die SQL Verbindung
        /// </summary>
        /// <returns></returns>
        public static bool Init()
        {
            if (SQL_CONNECTION != null)
                return true;
            Log("SQL initializing...");
            #region Standard ConnectionString erstellen

            //Server setzen
            if (String.IsNullOrEmpty(defaultInstance))
                CONNECTIONSTRING = "Data Source=" + defaultServer + ";";
            else
                CONNECTIONSTRING = "Data Source=" + defaultServer + "\\" + defaultInstance + ";";

            //datenbank
            //CONNECTIONSTRING += "Initial Catalog=" + defaultDB + ";";

            if (useWinAuth)
                CONNECTIONSTRING += "Integrated Security = SSPI;";
            else
                //nach Login Fragen, dann:
                //CONNECTIONSTRING += "User id=" + username + ";Password=" + password + ";";
                ;
            #endregion

            #region Verbindung prüfen
            Log("Testing SQL Connection... (CONNECTIONSTRING = " + CONNECTIONSTRING + ")");
            SqlConnection tmpCon = new SqlConnection(CONNECTIONSTRING);

            try
            {
                tmpCon.Open();
                tmpCon.Close();
                Log("Test succeeded!");
            }
            catch (Exception ex)
            {
                Log("Test failed!\n\t" + ex.Message);
                //nach Datenbankverbindung fragen
                return false;
            }


            Log("Check if iScream database exists...");
            if (!CheckIfDBExists())
            {
                Log("iScream database doesn't exist! Try to create database...");
                tmpCon.Open();

                List<string> querry = new List<string>
                {
                    "CREATE DATABASE [iScream]",
                    "CREATE TABLE [iScream].[dbo].[DBVersion]([Version] [varchar](50) NULL) ON [PRIMARY]",
                    "INSERT INTO [iScream].[dbo].[DBVersion] ([Version]) VALUES ('1.0')",
                    "CREATE TABLE [iScream].[dbo].[User]([User_id] [int] NOT NULL PRIMARY KEY, [Firstname] [varchar](max) NULL, [Lastname] [varchar](max) NULL) ON [PRIMARY]",
                    "CREATE TABLE [iScream].[dbo].[Games]([Game_id] [int] NOT NULL PRIMARY KEY, [Name] [varchar](max) NULL) ON [PRIMARY]",
                    "CREATE TABLE [iScream].[dbo].[Links]([User_id] [int] FOREIGN KEY REFERENCES [User](User_id),[Game_id] [int]  FOREIGN KEY REFERENCES Games(Game_id)) ON [PRIMARY]"
                };


                Log("Creation querry: ");
                foreach (string command in querry)
                    Log("\t" + command);

                foreach (string command in querry)
                {
                    using (SqlCommand cmd = new SqlCommand(command, tmpCon))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                tmpCon.Close();
                Log("iScream database created!");
            }
            else
                //prüfe Datenbankversion. falls geringer als aktuelle: anpassen
                Log("iScream database exists!");
            #endregion

            tmpCon.Dispose();

            Log("Add database to CONNECTIONSTRING...");

            //Datenbank zu CONNECTIONSTRING hinzufügen
            CONNECTIONSTRING += "Initial Catalog=" + defaultDB + ";";

            SQL_CONNECTION = new SqlConnection(CONNECTIONSTRING);

            SQL_CONNECTION.Open();

            Log("SQL initialized! Connection opened (CONNECTIONSTRING = " + CONNECTIONSTRING + ")");

            return true;
        }

        /// <summary>
        /// Schließt die SQL Verbindung
        /// </summary>
        public static void Close()
        {
            if (SQL_CONNECTION != null)
                SQL_CONNECTION.Close();
            Log("SQL closed!");
        }

        /// <summary>
        /// Gibt die von SQL belegten Resourcen frei
        /// </summary>
        public static void Dispose()
        {
            if (SQL_CONNECTION != null)
                SQL_CONNECTION.Close();
            SQL_CONNECTION.Dispose();
            Log("SQL disposed!");
        }

        /// <summary>
        /// Überprüft ob die iScream Datenbank auf dem verbundenen SQL Server existiert
        /// </summary>
        /// <returns></returns>
        private static bool CheckIfDBExists()
        {
            Log("Check started...");

            bool result = false;

            try
            {
                SqlConnection tmpCon = new SqlConnection(CONNECTIONSTRING);

                string querry = "SELECT database_id FROM sys.databases WHERE Name = 'iScream'";

                using (SqlCommand cmd = new SqlCommand(querry, tmpCon))
                {
                    tmpCon.Open();

                    int databaseID = 0;

                    object resultObj = cmd.ExecuteScalar();

                    if (resultObj != null)
                    {
                        int.TryParse(resultObj.ToString(), out databaseID);
                    }

                    tmpCon.Close();

                    result = (databaseID > 0);
                }

                Log("Check finished!");
            }
            catch (Exception ex)
            {
                Log("Check failed!\n\t" + ex.Message);
                result = false;
            }

            return result;
        }

        public static bool Insert(string table, string[] columns, object[] values)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = SQL_CONNECTION;

            if (columns.Count() != values.Count())
                return false;
            string cmdText = "INSERT INTO " + table + " (";
            string cmdValues = "VALUES (";
            int curVal = 0;
            foreach (string col in columns)
            {
                if (col != columns.First())
                {
                    cmdText += ", " + col;
                    cmdValues += ", @" + col;
                }
                else
                {
                    cmdText += col;
                    cmdValues += "@" + col;
                }
                cmd.Parameters.AddWithValue("@" + col, values[curVal++]);
            }
            cmdText += ") ";
            cmdValues += ")";

            cmd.CommandText = cmdText + cmdValues;
            return Convert.ToBoolean(cmd.ExecuteNonQuery());
        }

        public static DataRowCollection Select(string select, string from, string where)
        {
            string cmdText = "SELECT " + select + " ";
            cmdText += "FROM " + from;
            if (!String.IsNullOrEmpty(where))
                cmdText += " WHERE " + where;

            return Select(cmdText);
        }

        public static DataRowCollection Select(string select, string from, string where, string orderBy)
        {
            string cmdText = "SELECT " + select + " ";
            cmdText += "FROM " + from;
            if (!String.IsNullOrEmpty(where))
                cmdText += " WHERE " + where;
            if (!String.IsNullOrEmpty(orderBy))
                cmdText += " ORDER BY " + orderBy;

            return Select(cmdText);
        }

        public static DataRowCollection Select(string select, string from)
        {
            string cmdText = "SELECT " + select + " ";
            cmdText += "FROM " + from;

            return Select(cmdText);
        }

        public static DataRowCollection Select(string cmdText)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(cmdText, SQL_CONNECTION);

            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                sda.Fill(dt);

            return dt.Rows;
        }

        public static int Delete(string from, string where)
        {
            string cmdText = "DELETE FROM " + from;
            if (!String.IsNullOrEmpty(where))
                cmdText += " WHERE " + where;

            using (SqlCommand cmd = new SqlCommand(cmdText, SQL_CONNECTION))
                return cmd.ExecuteNonQuery();
        }

        public static int Update(string update, string[] set, object[] values)
        {
            return Update(update, set, values, "");
        }

        public static int Update(string update, string[] set, object[] values, string where)
        {
            if (set.Count() == 0 || (set.Count() != values.Count()))
                return -1;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = SQL_CONNECTION;

            string cmdText = "UPDATE " + update + " SET " + set[0] + " = @" + set[0];
            cmd.Parameters.AddWithValue("@" + set[0], values[0]);

            for (int i = 1; i < set.Count(); i++)
            {
                cmdText += ", " + set[i] + " = @" + set[i];
                cmd.Parameters.AddWithValue("@" + set[i], values[i]);
            }

            if (!String.IsNullOrEmpty(where))
                cmdText += " WHERE " + where;

            cmd.CommandText = cmdText;

            return cmd.ExecuteNonQuery();
        }
    }
}
