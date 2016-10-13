using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    class Datenhaltung1
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

        public static bool Init()
        {
            #region Standard ConnectionString erstellen
            //Server setzen
            if (defaultInstance.Length > 0)
                CONNECTIONSTRING = "Data Source=" + defaultServer + "\\" + defaultInstance + ";";
            else
                CONNECTIONSTRING = "Data Source=" + defaultServer + ";";

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
            SqlConnection tmpCon = new SqlConnection(CONNECTIONSTRING);

            try
            {
                tmpCon.Open();
                tmpCon.Close();
            }
            catch (Exception ex)
            {
                //nach Datenbankverbindung fragen
                return false;
            }

            if (!CheckIfDBExists())
            {
                tmpCon.Open();

                string querry = "";
                querry += "USE [master] GO ";
                querry += "CREATE DATABASE [iScream] ";
                querry += "USE [iScream] GO ";
                querry += "CREATE TABLE [dbo].[DBVersion]([Version] [varchar](50) NULL) ON [PRIMARY] ";
                querry += "CREATE TABLE [dbo].[Nutzer]([Nutzer_id] [int] NULL, [Vorname] [varchar](max) NULL, [Nachname] [varchar](max) NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ";
                querry += "CREATE TABLE [dbo].[Spiele]([Spiel_id] [int] NULL, [Name] [varchar](max) NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ";
                querry += "INSERT INTO [dbo].[DBVersion] ([Version]) VALUES ('1.0')";

                using (SqlCommand cmd = new SqlCommand(querry, tmpCon))
                {
                    cmd.ExecuteNonQuery();
                }

                tmpCon.Close();
            }
            #endregion

            tmpCon.Dispose();

            //Datenbank zu CONNECTIONSTRING hinzufügen
            CONNECTIONSTRING += "Initial Catalog=" + defaultDB + ";";

            SQL_CONNECTION = new SqlConnection(CONNECTIONSTRING);

            SQL_CONNECTION.Open();

            return true;
        }

        private static void Dispose()
        {
            if (SQL_CONNECTION != null)
                SQL_CONNECTION.Close();
        }

        private static bool CheckIfDBExists()
        {
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
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
    }
}
