using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream.Tests
{
    static class DatenhaltungTests
    {
        private static bool TestDatenhaltung1 = false;
        private static bool TestDatenhaltung2 = true;

        private static bool ReadLineAfterEachTest = true;

        private static List<User> testnutzer = new List<User>
        {
<<<<<<< HEAD
            new User("Klara","Himmel",1),
            new User("Mike", "Rohsoft",2),
            new User("Martin Router", "King", 3)
=======
            new User("Mike", "Rohsoft"),//,1),
            new User("Ann", "Droid"),//, 2),
            new User("Sam","Sung")//,3)
>>>>>>> refs/remotes/origin/Datenhaltung
        };

        private static List<Game> testspiele = new List<Game>
        {
<<<<<<< HEAD
            new Game("Tetris", 1),
            new Game("Minecraft", 2)
=======
            new Game("Tetris"),//, 1),
            new Game("Minecraft")//, 2)
>>>>>>> refs/remotes/origin/Datenhaltung
        };

        public static List<Link> testverknüpfungen = new List<Link>
        {
            new Link(1,1),
            new Link(2,1),
            new Link(3,1),
            new Link(1,2),
            new Link(3,2)
        };

        private static Datenhaltung1 datenhaltung1;
        private static Datenhaltung2 datenhaltung2;

        private static int testCount;
        private static int successCount;

        private static void Next()
        {
            if (ReadLineAfterEachTest)
            {
                Console.WriteLine("Eingabe zum Fortfahren...");
                Console.ReadLine();

                int currentLineCursor = Console.CursorTop - 1;
                Console.SetCursorPosition(0, Console.CursorTop - 2);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }
            else
                Console.WriteLine();
        }

        public static void Run(bool _ReadLineAfterEachTest)
        {
            ReadLineAfterEachTest = _ReadLineAfterEachTest;
            Run();
        }

        public static void Run()
        {
            Console.WriteLine("Starte DatenhaltungsTests!");
            Console.Write("Eingabe zum Fortfahren...");
            Console.ReadLine();
            Console.WriteLine();
            if (TestDatenhaltung1)
            {
                Console.WriteLine("Teste Datenhaltung1...");
                Next();
                Console.WriteLine("Test für Datenhaltung1 noch nicht implementiert!");
                Next();

                Console.WriteLine("Tests für Datenhaltung1 beendet! Ergebnis:");
                Console.WriteLine("\t" + successCount + " von " + testCount + " Tests erfolgreich abgeschlossen");

                Console.Write("Eingabe zum Fortfahren...");
                Console.ReadLine();
                Console.WriteLine();
            }

            if (TestDatenhaltung2)
            {
                Console.WriteLine("Teste Datenhaltung2...");
                Next();

                datenhaltung2 = new Datenhaltung2();

                Datenhaltung2Test();
                Next();

                Console.WriteLine("Tests für Datenhaltung2 beendet! Ergebnis:");
                Console.WriteLine("\t" + successCount + " von " + testCount + " Tests erfolgreich abgeschlossen");

                Console.Write("Eingabe zum Fortfahren...");
                Console.ReadLine();
                Console.WriteLine();
            }

            Console.WriteLine("DatenhaltungsTests abgeschlossen!");
            Console.Write("Eingabe zum Fortfahren...");
            Console.ReadLine();
        }

        public static void Datenhaltung2Test()
        {
            testCount = 0;
            successCount = 0;

            string databasePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "iScream\\XMLDatabase.xml");

            //create backup of database if exists
            bool createdBackup = System.IO.File.Exists(databasePath);
            if (createdBackup)
            {
                Console.Write("Erstelle Backup der vorhandenen Datenbank...");
                System.IO.File.Delete(databasePath.Replace(".", ".BACKUP."));
                System.IO.File.Move(databasePath, databasePath.Replace(".", ".BACKUP."));
                Console.WriteLine(" fertig.");
            }

            //test storing of data
            TestDataStoring(datenhaltung2);
            Next();
            //check stored data
            ValidateStoredData(datenhaltung2);
            Next();

            //restore backup of database if exists
            if (createdBackup)
            {
                Console.Write("Stelle Backup wieder her...");
                System.IO.File.Delete(databasePath);
                System.IO.File.Move(databasePath.Replace(".", ".BACKUP."), databasePath.Replace(".BACKUP.", "."));
                Console.WriteLine(" fertig.");
            }
        }

        public static bool TestDataStoring(IDatenhaltung datenhaltung)
        {
            testCount++;
            Console.WriteLine("Teste das Speichern von Daten... ");
            Next();
            try
            {
                Console.Write("Schreibe in Datenbank... ");
                datenhaltung.AddUser(testnutzer);

                datenhaltung.AddGame(testspiele);

                datenhaltung.AddLink(testverknüpfungen);

                Console.WriteLine("erfolg!");
                successCount++;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("fehler!\n\t" + ex.Message);
                return false;
            }
        }

        public static bool ValidateStoredData(IDatenhaltung datenhaltung)
        {
            testCount++;
            Console.WriteLine("Prüfe Daten aus Datenbank... ");
            Next();
            try
            {
                Console.Write("Lade Daten...");
<<<<<<< HEAD
                List<User> tmpNutzer = datenhaltung.HoleNutzer();
                List<Game> tmpSpiele = datenhaltung.HoleSpiel();
                List<Link> tmpVerknüpfung = datenhaltung.HoleVerknüpfung();
=======
                List<User> tmpNutzer = datenhaltung.GetUser();
                List<Game> tmpSpiele = datenhaltung.GetGame();
                List<Link> tmpVerknüpfung = datenhaltung.GetLink();
>>>>>>> refs/remotes/origin/Datenhaltung
                Console.WriteLine(" fertig.");
                Next();

                Console.WriteLine("Prüfe geladene Daten...");

                bool failed = false;

                #region Anzahlcheck
                if (tmpNutzer.Count != testnutzer.Count())
                {
                    failed = true;
                    Console.WriteLine("Anzahl der Nutzer stimmt nicht überein! Geladen: " + tmpNutzer.Count + ", gefordert: " + testnutzer.Count);
                }
                if (tmpSpiele.Count != testspiele.Count())
                {
                    failed = true;
                    Console.WriteLine("Anzahl der Spiele stimmt nicht überein! Geladen: " + tmpSpiele.Count + ", gefordert: " + testspiele.Count);
                }
                if (tmpVerknüpfung.Count != testverknüpfungen.Count())
                {
                    failed = true;
                    Console.WriteLine("Anzahl der Verknüpfungen stimmt nicht überein! Geladen: " + tmpVerknüpfung.Count + ", gefordert: " + testverknüpfungen.Count);
                }

                if (failed)
                    return false;
                #endregion

                #region Inhaltcheck
                Console.WriteLine("Nutzer:");
                for (int i = 0; i < testnutzer.Count; i++)
                    if (tmpNutzer[i].Name != testnutzer[i].Name || tmpNutzer[i].User_id != testnutzer[i].User_id)
                    {
                        Console.WriteLine("Daten stimmen nicht überein! Geladen: Nutzer(" + tmpNutzer[i].Firstname + "," + tmpNutzer[i].Lastname + "," + tmpNutzer[i].User_id + "), gefordert: Nutzer(" + testnutzer[i].Firstname + "," + testnutzer[i].Lastname + "," + testnutzer[i].User_id + ")");
                        failed = true;
                    }
                    else
                        Console.WriteLine("Datensatz korrekt! Nutzer(" + testnutzer[i].Firstname + "," + testnutzer[i].Lastname + "," + testnutzer[i].User_id + ")");
                Next();

                Console.WriteLine("Spiele:");
                for (int i = 0; i < testspiele.Count; i++)
                    if (tmpSpiele[i].Name != testspiele[i].Name || tmpSpiele[i].Game_id != testspiele[i].Game_id)
                    {
                        Console.WriteLine("Daten stimmen nicht überein! Geladen: Spiel(" + tmpSpiele[i].Name + "," + tmpSpiele[i].Game_id + "), gefordert: Spiel(" + testspiele[i].Name + "," + testspiele[i].Game_id + ")");
                        failed = true;
                    }
                    else
                        Console.WriteLine("Datensatz korrekt! Spiel(" + testspiele[i].Name + "," + testspiele[i].Game_id + ")");
                Next();

                Console.WriteLine("Verknüpfungen:");
                for (int i = 0; i < testverknüpfungen.Count; i++)
                    if (tmpVerknüpfung[i].User_id != testverknüpfungen[i].User_id || tmpVerknüpfung[i].Game_id != testverknüpfungen[i].Game_id)
                    {
                        Console.WriteLine("Daten stimmen nicht überein! Geladen: Verknüpfung(" + tmpVerknüpfung[i].User_id + "," + tmpVerknüpfung[i].Game_id + "), gefordert: Verknüpfung(" + testverknüpfungen[i].User_id + "," + testverknüpfungen[i].Game_id + ")");
                        failed = true;
                    }
                    else
                        Console.WriteLine("Datensatz korrekt! Verknüpfung(" + testverknüpfungen[i].User_id + "," + testverknüpfungen[i].Game_id + ")");
                Next();

                if (failed)
                    return false;
                #endregion

                Console.WriteLine("Alle Daten korrekt!");
                successCount++;

                return true;
            }
            catch (Exception ex) { return false; }
        }
    }
}
