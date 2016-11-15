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
        public int HoleNächsteNutzer_id()
        {
            DataRowCollection rows = SQL.Select("TOP 1 Nutzer_id", "Nutzer", "", "Nutzer_id DESC");
            if (rows.Count == 0)
                return 1;
            else
                return Convert.ToInt32(rows[0]["Nutzer_id"]) + 1;
        }

        public int HoleNächsteSpiel_id()
        {
            DataRowCollection rows = SQL.Select("TOP 1 Spiel_id", "Spiele", "", "Spiel_id DESC");
            if (rows.Count == 0)
                return 1;
            else
                return Convert.ToInt32(rows[0]["Spiel_id"]) + 1;
        }

        public Nutzer HoleNutzer(int nutzer_id)
        {
            DataRowCollection rows = SQL.Select("*", "Nutzer", "Nutzer_id = " + nutzer_id);
            if (rows.Count == 0)
                return null;
            else
                return new Nutzer(
                    Convert.ToString(rows[0]["Vorname"]),
                    Convert.ToString(rows[0]["Nachname"]),
                    Convert.ToInt32(rows[0]["Nutzer_id"])
                );
        }

        public List<Nutzer> HoleNutzer()
        {
            DataRowCollection rows = SQL.Select("*", "Nutzer");
            if (rows.Count == 0)
                return null;
            else
            {
                List<Nutzer> result = new List<Nutzer>();

                foreach (DataRow row in rows)
                    result.Add(new Nutzer(
                        Convert.ToString(row["Vorname"]),
                        Convert.ToString(row["Nachname"]),
                        Convert.ToInt32(row["Nutzer_id"])
                    ));
                return result;
            }
        }

        public Spiel HoleSpiel(int spiel_id)
        {
            DataRowCollection rows = SQL.Select("*", "Spiele", "Spiel_id = " + spiel_id);
            if (rows.Count == 0)
                return null;
            else
                return new Spiel(
                    Convert.ToString(rows[0]["Name"]),
                    Convert.ToInt32(rows[0]["Spiel_id"])
                );
        }

        public List<Spiel> HoleSpiel()
        {
            DataRowCollection rows = SQL.Select("*", "Spiele");
            if (rows.Count == 0)
                return null;
            else
            {
                List<Spiel> result = new List<Spiel>();

                foreach (DataRow row in rows)
                    result.Add(new Spiel(
                        Convert.ToString(row["Name"]),
                        Convert.ToInt32(row["Spiel_id"])
                    ));
                return result;
            }
        }

        public Verknüpfung HoleVerknüpfung(int nutzer_id, int spiel_id)
        {
            DataRowCollection rows = SQL.Select("*", "Verknüpfungen", "Nutzer_id = " + nutzer_id + "AND Spiel_id = " + spiel_id);
            if (rows.Count == 0)
                return null;
            else
                return new Verknüpfung(
                    Convert.ToInt32(rows[0]["Nutzer_id"]),
                    Convert.ToInt32(rows[0]["Spiel_id"])
                );
        }

        public Verknüpfung HoleVerknüpfung(Verknüpfung verknüpfung)
        {
            return HoleVerknüpfung(verknüpfung.Nutzer_id, verknüpfung.Spiel_id);
        }

        public List<Verknüpfung> HoleVerknüpfung()
        {

            DataRowCollection rows = SQL.Select("*", "Verknüpfungen");
            if (rows.Count == 0)
                return null;
            else
            {
                List<Verknüpfung> result = new List<Verknüpfung>();

                foreach (DataRow row in rows)
                    result.Add(new Verknüpfung(
                        Convert.ToInt32(row["Nutzer_id"]),
                        Convert.ToInt32(row["Spiel_id"])
                    ));
                return result;
            }
        }

        public List<Spiel> HoleSpieleVonNutzer(int nutzer_id)
        {
            DataRowCollection rows = SQL.Select("Spiel_id", "Verknüpfungen", "Nutzer_id = " + nutzer_id);
            if (rows.Count == 0)
                return null;
            else
            {
                List<Spiel> result = new List<Spiel>();

                foreach (DataRow row in rows)
                    result.Add(HoleSpiel(Convert.ToInt32(row[0])));

                return result;
            }
        }

        public List<Spiel> HoleSpieleVonNutzer(Nutzer nutzer)
        {
            return HoleSpieleVonNutzer(nutzer.Nutzer_id);
        }

        public List<Nutzer> HoleNutzerVonSpiel(int spiel_id)
        {
            DataRowCollection rows = SQL.Select("Nutzer_id", "Verknüpfungen", "Spiel_id = " + spiel_id);
            if (rows.Count == 0)
                return null;
            else
            {
                List<Nutzer> result = new List<Nutzer>();

                foreach (DataRow row in rows)
                    result.Add(HoleNutzer(Convert.ToInt32(row[0])));

                return result;
            }
        }

        public List<Nutzer> HoleNutzerVonSpiel(Spiel spiel)
        {
            return HoleNutzerVonSpiel(spiel.Spiel_id);
        }
        #endregion

        #region Hinzufügen
        public bool FügeNutzerHinzu(string vorname, string nachname, int nutzer_id)
        {
            return FügeNutzerHinzu(new Nutzer(vorname, nachname, nutzer_id));
        }

        public bool FügeNutzerHinzu(string vorname, string nachname)
        {
            return FügeNutzerHinzu(new Nutzer(vorname, nachname, HoleNächsteNutzer_id()));
        }

        public bool FügeNutzerHinzu(Nutzer nutzer)
        {
            if (nutzer.Nutzer_id == -1)
                nutzer.Nutzer_id = HoleNächsteNutzer_id();

            return SQL.Insert("Nutzer", new string[] { "Vorname", "Nachname", "Nutzer_id" }, new object[] { nutzer.Vorname, nutzer.Nachname, nutzer.Nutzer_id });
        }

        public void FügeNutzerHinzu(List<Nutzer> nutzer)
        {
            foreach (Nutzer cur in nutzer)
                FügeNutzerHinzu(cur);
        }

        public bool FügeSpielHinzu(string name, int spiel_id)
        {
            return FügeSpielHinzu(new Spiel(name, spiel_id));
        }

        public bool FügeSpielHinzu(string name)
        {
            return FügeSpielHinzu(new Spiel(name, HoleNächsteSpiel_id()));
        }

        public bool FügeSpielHinzu(Spiel spiel)
        {
            if (spiel.Spiel_id == -1)
                spiel.Spiel_id = HoleNächsteSpiel_id();

            return SQL.Insert("Spiele", new string[] { "Name", "Spiel_id" }, new object[] { spiel.Name, spiel.Spiel_id });
        }

        public void FügeSpielHinzu(List<Spiel> spiele)
        {
            foreach (Spiel cur in spiele)
                FügeSpielHinzu(cur);
        }

        public bool FügeVerknüpfungHinzu(int nutzer_id, int spiel_id)
        {
            return FügeVerknüpfungHinzu(new Verknüpfung(nutzer_id, spiel_id));
        }

        public bool FügeVerknüpfungHinzu(Verknüpfung verknüpfung)
        {
            return SQL.Insert("Verknüpfungen", new string[] { "Nutzer_id", "Spiel_id" }, new object[] { verknüpfung.Nutzer_id, verknüpfung.Spiel_id });
        }

        public void FügeVerknüpfungHinzu(List<Verknüpfung> verknüpfungen)
        {
            foreach (Verknüpfung cur in verknüpfungen)
                FügeVerknüpfungHinzu(cur);
        }
        #endregion

        #region Löschen
        public bool LöscheNutzer(int nutzer_id)
        {
            if (Convert.ToInt32(SQL.Delete("Nutzer", "Nutzer_id = " + nutzer_id)) > 0)
            {
                SQL.Delete("Verknüpfungen", "Nutzer_id = " + nutzer_id);
                return true;
            }
            else
                return false;
        }

        public bool LöscheNutzer(Nutzer nutzer)
        {
            return LöscheNutzer(nutzer.Nutzer_id);
        }

        public void LöscheNutzer(List<Nutzer> nutzer)
        {
            foreach (Nutzer cur in nutzer)
                LöscheNutzer(cur.Nutzer_id);
        }

        public bool LöscheSpiel(int spiel_id)
        {

            if (Convert.ToInt32(SQL.Delete("Spiele", "Spiel_id = " + spiel_id)) > 0)
            {
                SQL.Delete("Verknüpfungen", "Spiel_id = " + spiel_id);
                return true;
            }
            else
                return false;
        }

        public bool LöscheSpiel(Spiel spiel)
        {
            return LöscheSpiel(spiel.Spiel_id);
        }

        public void LöscheSpiel(List<Spiel> spiele)
        {
            foreach (Spiel cur in spiele)
                LöscheNutzer(cur.Spiel_id);
        }

        public bool LöscheVerknüpfung(int nutzer_id, int spiel_id)
        {
            return Convert.ToInt32(SQL.Delete("Verknüpfungen", "Nutzer_id = " + nutzer_id + " AND Spiel_id = " + spiel_id)) > 0;
        }

        public bool LöscheVerknüpfung(Verknüpfung verknüpfung)
        {
            return LöscheVerknüpfung(verknüpfung.Nutzer_id, verknüpfung.Spiel_id);
        }
        #endregion

        #region Ändern
        public bool ÄndereNutzer(int nutzer_id, string vorname, string nachname)
        {
            if (SQL.Select("Nutzer_id", "Nutzer", "Nutzer_id = " + nutzer_id).Count == 0)
                return false;

            SQL.Update("Nutzer", new string[] { "Vorname", "Nachname" }, new object[] { vorname, nachname }, "Nutzer_id = " + nutzer_id);

            return true;
        }

        public bool ÄndereNutzer(Nutzer nutzer)
        {
            return ÄndereNutzer(nutzer.Nutzer_id, nutzer.Vorname, nutzer.Nachname);
        }


        public bool ÄndereSpiel(int spiel_id, string name)
        {
            if (SQL.Select("Spiel_id", "Spiele", "Spiel_id = " + spiel_id).Count > 0)
                return false;

            SQL.Update("Spiele", new string[] { "Name" }, new object[] { name }, "Spiel_id = " + spiel_id);

            return true;
        }

        public bool ÄndereSpiel(Spiel spiel)
        {
            return ÄndereSpiel(spiel.Spiel_id, spiel.Name);
        }
        #endregion

        #region Suchen
        public List<Nutzer> SucheNutzer(string vorname, string nachname)
        {
            List<Nutzer> result = new List<Nutzer>();

            string cmdText = "SELECT * FROM Nutzer WHERE ";

            if(!String.IsNullOrEmpty(vorname))
                cmdText += "Vorname like '%"+vorname+"%'";

            if(!String.IsNullOrEmpty(nachname))
                if(!String.IsNullOrEmpty(vorname))
                    cmdText+= " && Nachname like '%"+nachname+"%'";
                else 
                    cmdText += "Nachname like '%"+nachname+"%'";

            DataRowCollection rows = SQL.Select(cmdText);

            foreach (DataRow row in rows)
                result.Add(new Nutzer(Convert.ToString(row["Vorname"]), Convert.ToString(row["Nachname"]), Convert.ToInt32(row["Nutzer_id"])));

            return result;
        }

        public List<Spiel> SucheSpiel(string name)
        {
            List<Spiel> result = new List<Spiel>();

            string cmdText = "SELECT * FROM Spiele WHERE ";
            if (!String.IsNullOrEmpty(name))
                cmdText += "Name like '%" + name + "%'";

            DataRowCollection rows = SQL.Select(cmdText);

            foreach (DataRow row in rows)
                result.Add(new Spiel(Convert.ToString(row["Name"]), Convert.ToInt32(row["Nutzer_id"])));

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
                    "CREATE TABLE [iScream].[dbo].[Nutzer]([Nutzer_id] [int] NULL, [Vorname] [varchar](max) NULL, [Nachname] [varchar](max) NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                    "CREATE TABLE [iScream].[dbo].[Spiele]([Spiel_id] [int] NULL, [Name] [varchar](max) NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
                    "CREATE TABLE [iScream].[dbo].[Verknüpfungen]([Nutzer_id] [int] NULL,[Spiel_id] [int] NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]"
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
            string cmdText = "FROM " + from;
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
