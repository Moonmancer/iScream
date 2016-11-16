using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace iScream
{
    class Datenhaltung1 : IDatenhaltung
    {
        public Datenhaltung1()
        {
            SQL.Init();
        }

        #region Holen
        public int HoleNächsteNutzer_id()
        {
            throw new NotImplementedException();
        }

        public int HoleNächsteSpiel_id()
        {
            throw new NotImplementedException();
        }

        public User HoleNutzer(int nutzer_id)
        {
            throw new NotImplementedException();
        }

        public List<User> HoleNutzer()
        {
            throw new NotImplementedException();
        }

        public Game HoleSpiel(int spiel_id)
        {
            throw new NotImplementedException();
        }

        public List<Game> HoleSpiel()
        {
            throw new NotImplementedException();
        }

        public List<Link> HoleVerknüpfung()
        {
            throw new NotImplementedException();
        }

        public List<Game> HoleSpieleVonNutzer(int nutzer_id)
        {
            throw new NotImplementedException();
        }

        public List<Game> HoleSpieleVonNutzer(User nutzer)
        {
            throw new NotImplementedException();
        }

        public List<User> HoleNutzerVonSpiel(int spiel_id)
        {
            throw new NotImplementedException();
        }

        public List<User> HoleNutzerVonSpiel(Game spiel)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Hinzufügen
        public bool FügeNutzerHinzu(string vorname, string nachname, int nutzer_id)
        {
            throw new NotImplementedException();
        }

        public bool FügeNutzerHinzu(User nutzer)
        {
            throw new NotImplementedException();
        }

        public void FügeNutzerHinzu(List<User> nutzer)
        {
            throw new NotImplementedException();
        }

        public bool FügeSpielHinzu(string name, int spiel_id)
        {
            throw new NotImplementedException();
        }

        public bool FügeSpielHinzu(Game spiel)
        {
            throw new NotImplementedException();
        }

        public void FügeSpielHinzu(List<Game> spiele)
        {
            throw new NotImplementedException();
        }

        public bool FügeVerknüpfungHinzu(int nutzer_id, int spiel_id)
        {
            throw new NotImplementedException();
        }

        public bool FügeVerknüpfungHinzu(Link verknüpfung)
        {
            throw new NotImplementedException();
        }

        public void FügeVerknüpfungHinzu(List<Link> verknüpfungen)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Löschen
        #endregion

        #region Ändern
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
        static string defaultServer = ".";

        static string defaultDB = "iScream";

        static string defaultInstance;

        static bool useWinAuth = true;

        static string defaultUsername = "sa";
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
                    "CREATE TABLE [iScream].[dbo].[Nutzer]([Nutzer_id] [int] NULL, [Vorname] [varchar](max) NULL, [Nachname] [varchar](max) NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                    "CREATE TABLE [iScream].[dbo].[Spiele]([Spiel_id] [int] NULL, [Name] [varchar](max) NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]"
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
    }
}
