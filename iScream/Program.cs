using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    class Program
    {


        static void Main(string[] args)
        {
            //Main should always start with Settings.Load()
            Settings.Load();

            DHTUI dhtui = new DHTUI(new Datenhaltung2());

            dhtui.Run();

            //Main should always end with Settings.Save()
            Settings.Save();
        }

        public static void CopyData(IDatenhaltung from, IDatenhaltung to)
        {
            to.AddUser(from.GetUser());
            to.AddGame(from.GetGame());
            to.AddLink(from.GetLink());
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