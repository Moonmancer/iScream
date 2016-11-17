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


            List<User> nutzers = new List<User>
            {
                new User("Mike", "Rohsoft"),
                new User("Ann", "Droid"),
                new User("Sam", "Sung")
            };

            List<Game> spiele = new List<Game>
            {
                new Game("Tetris"),
                new Game("Minecraft")
            };

            List<Link> verknüpfungen = new List<Link>
            {
                new Link(2,1),
                new Link(2,2),
                new Link(3,1),
                new Link(3,2)
            };

            IDatenhaltung dh = new Datenhaltung1();

            dh.AddUser(nutzers);
            dh.AddGame(spiele);

            DisplayUsers(dh);

            Console.WriteLine("\nÄndere Nutzer 1...");
            dh.UpdateUser(1, "Maik", "Rosoft");

            Console.WriteLine();
            DisplayUsers(dh);

            Console.WriteLine("\nLösche Nutzer 1...");
            dh.DeleteUser(1);

            Console.WriteLine();
            DisplayUsers(dh);

            Console.WriteLine();
            dh.AddLink(verknüpfungen);
            DisplayLinks(dh);

            Console.WriteLine("\nLösche Nutzer 2...");
            dh.DeleteUser(2);

            Console.WriteLine();
            DisplayLinks(dh);

            Console.ReadLine();
        }
        public static void DisplayUsers(IDatenhaltung dh)
        {
            Console.WriteLine("Nutzer:");
            foreach (User nutzer in dh.GetUser())
                Console.WriteLine("\tID: " + nutzer.User_id + "\tVorname: " + nutzer.Firstname + "\tNachname: " + nutzer.Lastname);
        }

        public static void DisplayUser(IDatenhaltung dh, int nutzer_id)
        {
            User nutzer = dh.GetUser(nutzer_id);
            Console.WriteLine("ID: " + nutzer.User_id + "\tVorname: " + nutzer.Firstname + "\tNachname: " + nutzer.Lastname);
        }

        public static void DisplayGames(IDatenhaltung dh)
        {
            Console.WriteLine("Nutzer:");
            foreach (User nutzer in dh.GetUser())
                Console.WriteLine("\tID: " + nutzer.User_id + "\tVorname: " + nutzer.Firstname + "\tNachname: " + nutzer.Lastname);
        }

        public static void DisplayLinks(IDatenhaltung dh)
        {
            Console.WriteLine("Verknüpfungen:");
            foreach (Link link in dh.GetLink())
                Console.WriteLine("\tNutzer: " + dh.GetUser(link.User_id) + "\tSpiel: " + dh.GetGame(link.Game_id));
        }
    }
}
