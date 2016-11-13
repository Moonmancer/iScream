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

        private static List<Nutzer> testnutzer = new List<Nutzer>
        {
            new Nutzer("Mike", "Rohsoft"),//,1),
            new Nutzer("Ann", "Droid"),//, 2),
            new Nutzer("Sam","Sung")//,3)
        };

        private static List<Spiel> testspiele = new List<Spiel>
        {
            new Spiel("Tetris"),//, 1),
            new Spiel("Minecraft")//, 2)
        };

        public static List<Verknüpfung> testverknüpfungen = new List<Verknüpfung>
        {
            new Verknüpfung(1,1),
            new Verknüpfung(2,1),
            new Verknüpfung(3,1),
            new Verknüpfung(1,2),
            new Verknüpfung(3,2)
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
                datenhaltung.FügeNutzerHinzu(testnutzer);

                datenhaltung.FügeSpielHinzu(testspiele);

                datenhaltung.FügeVerknüpfungHinzu(testverknüpfungen);

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
                List<Nutzer> tmpNutzer = datenhaltung.HoleNutzer();
                List<Spiel> tmpSpiele = datenhaltung.HoleSpiel();
                List<Verknüpfung> tmpVerknüpfung = datenhaltung.HoleVerknüpfung();
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
                    if (tmpNutzer[i].Name != testnutzer[i].Name || tmpNutzer[i].Nutzer_id != testnutzer[i].Nutzer_id)
                    {
                        Console.WriteLine("Daten stimmen nicht überein! Geladen: Nutzer(" + tmpNutzer[i].Vorname + "," + tmpNutzer[i].Nachname + "," + tmpNutzer[i].Nutzer_id + "), gefordert: Nutzer(" + testnutzer[i].Vorname + "," + testnutzer[i].Nachname + "," + testnutzer[i].Nutzer_id + ")");
                        failed = true;
                    }
                    else
                        Console.WriteLine("Datensatz korrekt! Nutzer(" + testnutzer[i].Vorname + "," + testnutzer[i].Nachname + "," + testnutzer[i].Nutzer_id + ")");
                Next();

                Console.WriteLine("Spiele:");
                for (int i = 0; i < testspiele.Count; i++)
                    if (tmpSpiele[i].Name != testspiele[i].Name || tmpSpiele[i].Spiel_id != testspiele[i].Spiel_id)
                    {
                        Console.WriteLine("Daten stimmen nicht überein! Geladen: Spiel(" + tmpSpiele[i].Name + "," + tmpSpiele[i].Spiel_id + "), gefordert: Spiel(" + testspiele[i].Name + "," + testspiele[i].Spiel_id + ")");
                        failed = true;
                    }
                    else
                        Console.WriteLine("Datensatz korrekt! Spiel(" + testspiele[i].Name + "," + testspiele[i].Spiel_id + ")");
                Next();

                Console.WriteLine("Verknüpfungen:");
                for (int i = 0; i < testverknüpfungen.Count; i++)
                    if (tmpVerknüpfung[i].Nutzer_id != testverknüpfungen[i].Nutzer_id || tmpVerknüpfung[i].Spiel_id != testverknüpfungen[i].Spiel_id)
                    {
                        Console.WriteLine("Daten stimmen nicht überein! Geladen: Verknüpfung(" + tmpVerknüpfung[i].Nutzer_id + "," + tmpVerknüpfung[i].Spiel_id + "), gefordert: Verknüpfung(" + testverknüpfungen[i].Nutzer_id + "," + testverknüpfungen[i].Spiel_id + ")");
                        failed = true;
                    }
                    else
                        Console.WriteLine("Datensatz korrekt! Verknüpfung(" + testverknüpfungen[i].Nutzer_id + "," + testverknüpfungen[i].Spiel_id + ")");
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
