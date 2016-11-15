using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    class Program
    {


        static void Main(string[] args)
        {

            //Tests.DatenhaltungTests.Run(false);

            
            List<Nutzer> nutzers = new List<Nutzer>
            {
                new Nutzer("Mike", "Rohsoft"),
                new Nutzer("Ann", "Droid"),
                new Nutzer("Sam", "Sung")
            };

            List<Spiel> spiele = new List<Spiel>
            {
                new Spiel("Tetris"),
                new Spiel("Minecraft")
            };

            IDatenhaltung dh = new Datenhaltung2();

            dh.FügeNutzerHinzu(nutzers);
            dh.FügeSpielHinzu(spiele);

            DisplayUsers(dh);

            Console.WriteLine("\nÄndere Nutzer 1...");
            dh.ÄndereNutzer(1, "Maik", "Rosoft");

            Console.WriteLine();
            DisplayUsers(dh);

            Console.WriteLine("\nLösche Nutzer 1...");
            dh.LöscheNutzer(1);

            Console.WriteLine();
            DisplayUsers(dh);

            Console.ReadLine();
        }
        public static void DisplayUsers(IDatenhaltung dh)
        {
            Console.WriteLine("Nutzer:");
            foreach (Nutzer nutzer in dh.HoleNutzer())
                Console.WriteLine("\tID: " + nutzer.Nutzer_id + "\tVorname: " + nutzer.Vorname + "\tNachname: " + nutzer.Nachname);
        }

        public static void DisplayUser(IDatenhaltung dh, int nutzer_id)
        {
            Nutzer nutzer = dh.HoleNutzer(nutzer_id);
            Console.WriteLine("ID: " + nutzer.Nutzer_id + "\tVorname: " + nutzer.Vorname + "\tNachname: " + nutzer.Nachname);
        }

        public static void DisplayGames(IDatenhaltung dh)
        {
            Console.WriteLine("Nutzer:");
            foreach (Nutzer nutzer in dh.HoleNutzer())
                Console.WriteLine("\tID: " + nutzer.Nutzer_id + "\tVorname: " + nutzer.Vorname + "\tNachname: " + nutzer.Nachname);
        }
    }
}
