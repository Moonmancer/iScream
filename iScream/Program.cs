using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace iScream
{
    class Program
    {


        static void Main(string[] args)
        {
            //Main should always start with Settings.Load()
            Settings.Load();

            Datenhaltung2 dh2 = new Datenhaltung2();
            DHTUI dhtui = new DHTUI(dh2);

            dhtui.Run();

            //Main should always end with Settings.Save()
            ToCSV(dh2, ".");
            Settings.Save();
        }

        public static void CopyData(IDatenhaltung from, IDatenhaltung to)
        {
            to.AddUser(from.GetUser());
            to.AddGame(from.GetGame());
            to.AddLink(from.GetLink());
        }

        public static void ToCSV(IDatenhaltung from, string toFolder)
        {
            DateTime dateTime = DateTime.Now;
            string dateString = dateTime.ToString("_yyyyMMdd_hhmm");

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
                        if (newline == "")
                            newline = typeof(User).GetProperty(prop.Name).GetValue(cur).ToString();
                        else
                            newline += ";" + typeof(User).GetProperty(prop.Name).GetValue(cur).ToString();

                CSV.AppendLine(newline);
            }
            System.IO.File.WriteAllText(System.IO.Path.Combine(toFolder, "User" + dateString + ".csv"), CSV.ToString());

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
                    if (newline == "")
                        newline = typeof(Game).GetProperty(prop.Name).GetValue(cur).ToString();
                    else
                        newline += ";" + typeof(Game).GetProperty(prop.Name).GetValue(cur).ToString();

                CSV.AppendLine(newline);
            }
            System.IO.File.WriteAllText(System.IO.Path.Combine(toFolder, "Games" + dateString + ".csv"), CSV.ToString());

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
                    if (newline == "")
                        newline = typeof(Link).GetProperty(prop.Name).GetValue(cur).ToString();
                    else
                        newline += ";" + typeof(Link).GetProperty(prop.Name).GetValue(cur).ToString();

                CSV.AppendLine(newline);
            }
            System.IO.File.WriteAllText(System.IO.Path.Combine(toFolder, "Links" + dateString + ".csv"), CSV.ToString());
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