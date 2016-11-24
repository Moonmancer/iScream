using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    class DatenhaltungExtras
    {

        public static void CopyData(IDatenhaltung from, IDatenhaltung to)
        {
            to.AddUser(from.GetUser());
            to.AddGame(from.GetGame());
            to.AddLink(from.GetLink());
        }

        public static void ExportCSV(IDatenhaltung from, string toFolder)
        {
            Directory.CreateDirectory(toFolder);

            PropertyInfo[] userProps = typeof(User).GetProperties();
            PropertyInfo[] gameProps = typeof(Game).GetProperties();
            PropertyInfo[] linkProps = typeof(Link).GetProperties();

            //User
            StringBuilder CSV = new StringBuilder();

            string firstline = "";
            foreach (PropertyInfo prop in userProps)
                if (prop.Name != "Name")
                    if (firstline == "")
                        firstline = prop.Name;
                    else
                        firstline += ";" + prop.Name;
            CSV.AppendLine(firstline);

            foreach (User cur in from.GetUser())
            {
                string newline = "";

                foreach (PropertyInfo prop in userProps)
                    if (prop.Name != "Name")
                        if (String.IsNullOrEmpty(newline))
                            newline = typeof(User).GetProperty(prop.Name).GetValue(cur).ToString();
                        else
                            newline += ";" + typeof(User).GetProperty(prop.Name).GetValue(cur).ToString();

                CSV.AppendLine(newline);
            }
            File.WriteAllText(Path.Combine(toFolder, "User.csv"), CSV.ToString());

            //Game
            CSV = new StringBuilder();

            firstline = "";
            foreach (PropertyInfo cur in gameProps)
                if (firstline == "")
                    firstline = cur.Name;
                else
                    firstline += ";" + cur.Name;
            CSV.AppendLine(firstline);

            foreach (Game cur in from.GetGame())
            {
                string newline = "";

                foreach (PropertyInfo prop in gameProps)
                    if (String.IsNullOrEmpty(newline))
                        newline = typeof(Game).GetProperty(prop.Name).GetValue(cur).ToString();
                    else
                        newline += ";" + typeof(Game).GetProperty(prop.Name).GetValue(cur).ToString();

                CSV.AppendLine(newline);
            }
            File.WriteAllText(Path.Combine(toFolder, "Games.csv"), CSV.ToString());

            //Link
            CSV = new StringBuilder();

            firstline = "";
            foreach (PropertyInfo cur in linkProps)
                if (firstline == "")
                    firstline = cur.Name;
                else
                    firstline += ";" + cur.Name;
            CSV.AppendLine(firstline);

            foreach (Link cur in from.GetLink())
            {
                string newline = "";

                foreach (PropertyInfo prop in linkProps)
                    if (String.IsNullOrEmpty(newline))
                        newline = typeof(Link).GetProperty(prop.Name).GetValue(cur).ToString();
                    else
                        newline += ";" + typeof(Link).GetProperty(prop.Name).GetValue(cur).ToString();

                CSV.AppendLine(newline);
            }
            File.WriteAllText(Path.Combine(toFolder, "Links.csv"), CSV.ToString());
        }

        public static IDatenhaltung ImportCSV(string fromPath, IDatenhaltung to)
        {
            DataSet data = new DataSet("iScream");

            if (File.Exists(Path.Combine(fromPath, "User.csv")))
                foreach (DataRow row in CSVtoDataTable(Path.Combine(fromPath, "User.csv"), "User").Rows)
                    to.AddUser(row["Firstname"].ToString(), row["Lastname"].ToString());

            if (File.Exists(Path.Combine(fromPath, "Games.csv")))
                foreach (DataRow row in CSVtoDataTable(Path.Combine(fromPath, "Games.csv"), "Games").Rows)
                    to.AddGame(row["Name"].ToString());

            if (File.Exists(Path.Combine(fromPath, "Links.csv")))
                foreach (DataRow row in CSVtoDataTable(Path.Combine(fromPath, "Links.csv"), "Links").Rows)
                    to.AddLink(Convert.ToInt32(row["User_id"]), Convert.ToInt32(row["Game_id"]));

            return to;
        }

        internal static DataTable CSVtoDataTable(string filePath, string tableName)
        {
            if (!File.Exists(filePath))
                return null;

            DataTable dt = new DataTable(tableName);
            StreamReader sr = new StreamReader(filePath);

            string line = sr.ReadLine();
            foreach (string cur in line.Split(';'))
                dt.Columns.Add(cur);

            while ((line = sr.ReadLine()) != null)
                dt.Rows.Add(line.Split(';'));

            return dt;
        }

        #region unused code to display data of IDatenhaltung
        public static void DisplayUser(IDatenhaltung dh)
        {
            Console.WriteLine("Nutzer:");
            foreach (User nutzer in dh.GetUser())
                Console.WriteLine("\tID: " + nutzer.User_id + "\tVorname: " + nutzer.Firstname + "\tNachname: " + nutzer.Lastname);
        }

        public static void DisplayUser(IDatenhaltung dh, int user_id)
        {
            User nutzer = dh.GetUser(user_id);
            Console.WriteLine("ID: " + nutzer.User_id + "\n\tVorname: " + nutzer.Firstname + "\tNachname: " + nutzer.Lastname);
        }

        public static void DisplayGame(IDatenhaltung dh)
        {
            Console.WriteLine("Spiele:");
            foreach (Game game in dh.GetGame())
                Console.WriteLine("\tID: " + game.Game_id + "\tname: " + game.Name);
        }

        public static void DisplayGame(IDatenhaltung dh, int game_id)
        {
            Console.WriteLine("ID: " + game_id + "\n\tname: " + dh.GetGame(game_id).Name);
        }

        public static void DisplayLinks(IDatenhaltung dh)
        {
            Console.WriteLine("Verknüpfungen:");
            foreach (Link link in dh.GetLink())
                Console.WriteLine("\tNutzer: " + dh.GetUser(link.User_id) + "\tSpiel: " + dh.GetGame(link.Game_id));
        }
        #endregion
    }
}
