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
    }

    static class SQL
    {
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

        static bool WRITE_LOG = true;

        static List<string> logLines = new List<string>();

        private static void Log(string text)
        {
            if (WRITE_LOG)
                logLines.Add(text);
        }

        public static void SaveLogToFile(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string fileName = Path.Combine(path, @"SQL_LOG_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".txt");
            File.WriteAllLines(fileName, logLines);
            System.Diagnostics.Process.Start(fileName);
        }

        public static bool Init()
        {
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

        public static void Close()
        {
            if (SQL_CONNECTION != null)
                SQL_CONNECTION.Close();
            Log("SQL closed!");
        }

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
                Log("Test failed!\n\t" + ex.Message);
                result = false;
            }

            return result;
        }
    }
}
