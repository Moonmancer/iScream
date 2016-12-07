using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream.TUI
{
    class TUI
    {
        //TODO: Ich darf nicht auf die IDatenhaltung zugreifen!!!
        private IFachkonzept _fachkonzept;
        private IDatenhaltung _datenhaltung;

        public TUI(IFachkonzept fachkonzept, IDatenhaltung datenhaltung)
        {
            _fachkonzept = fachkonzept;
            _datenhaltung = datenhaltung;
            MainMenu();
        }

        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Hauptmenu: \r\nBitte Wählen Sie eine Funktion aus:\r\n");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Nutzer anzeigen \t \t (a)");
            Console.WriteLine("Nutzer suchen \t \t (b)");
            Console.WriteLine("Nutzer hinzufügen \t \t (c)");
            Console.WriteLine("Nutzer bearbeiten \t \t (d)");
            Console.WriteLine("Nutzer löschen \t \t (e)");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Spiele anzeigen \t \t (f)");
            Console.WriteLine("Spiel suchen \t \t (g)");
            Console.WriteLine("Spiel hinzufügen \t \t (h)");
            Console.WriteLine("Spiel bearbeiten \t \t (i)");
            Console.WriteLine("Spiel löschen \t \t (j)");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Spiel hinzufügen \t \t (k)");
            Console.WriteLine("Spiel löschen \t \t (l)");
            Console.WriteLine("Beenden \t \t (x)");

            var auswahl = Convert.ToChar(Console.ReadLine());

            switch (auswahl)
            {
                case 'a':
                    Console.Clear();
                    UserDisplay();
                    break;
                case 'b':
                    Console.Clear();
                    UserSearch();
                    break;
                case 'c':
                    Console.Clear();
                    UserAdd();
                    break;
                case 'd':
                    Console.Clear();
                    UserChange();
                    break;
                case 'e':
                    Console.Clear();
                    UserDelete();
                    break;
                case 'f':
                    Console.Clear();
                    GameDisplay();
                    break;
                case 'g':
                    Console.Clear();
                    GameSearch();
                    break;
                case 'h':
                    Console.Clear();
                    GameAdd();
                    break;
                case 'i':
                    Console.Clear();
                    GameChange();
                    break;
                case 'j':
                    Console.Clear();
                    GameDelete();
                    break;
                case 'k':
                    Console.Clear();
                    Console.Write("Wieviel Liter moechten Sie einfuellen: ");
                    break;
                case 'l':
                    Console.Clear();
                    Console.Write("Wieviel Liter moechten Sie einfuellen: ");
                    break;
                case 'x':
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                default:
                    Console.Write("Bitte tätigen Sie eine valide Eingabe");
                    break;
            }
        }

        #region User Functions
        public void UserDisplay()
        {
            Console.WriteLine("Der Display mehrerer User ist nicht Implementiert:");
            var userList = _datenhaltung.GetUser();
            foreach (var user in userList)
            {
                Console.WriteLine("Die ausgewählten Userdaten:");
                Console.WriteLine(user.Name + "\r" + user.User_id + "\n");
            }
            Console.WriteLine("Beenden \t \t (x)");
            var end = Convert.ToChar(Console.ReadLine());
            if (end == 'x')
                MainMenu();

        }

        public void UserSearch()
        {
            Console.WriteLine("Zum suchen nach einen User werden einige Daten benötigt:");
            Console.WriteLine("Bitte geben Sie einen Vornamen ein:");
            var firstname = Console.ReadLine();
            Console.WriteLine("Bitte geben Sie einen Nachnamen ein:");
            var lastname = Console.ReadLine();
            var userList = _fachkonzept.searchUser(firstname, lastname);

            foreach (var user in userList)
            {
                Console.WriteLine("Die ausgewählten Userdaten:");
                Console.WriteLine(user.Name + "\r" + user.User_id + "\n");
            }
            Console.WriteLine("Beenden \t \t (x)");
            var end = Convert.ToChar(Console.ReadLine());
        }

        public void UserAdd()
        {
            Console.WriteLine("Zum hinzufügen eines Users werden einige Daten benötigt:");
            Console.WriteLine("Bitte geben Sie einen Vornamen ein:");
            var firstname = Console.ReadLine();
            Console.WriteLine("Bitte geben Sie einen Nachnamen ein:");
            var lastname = Console.ReadLine();
            var user = new User(firstname, lastname);

            if (_fachkonzept.createUser(user) == true)
            {
                Console.WriteLine("Der ausgewählte User wurde erstellt.");
                MainMenu();
            }
            else
            {
                Console.WriteLine("Der ausgewählte User konnte nicht erstellt werden.");
                MainMenu();
            }
        }

        public void UserChange()
        {
            Console.WriteLine("Bitte geben Sie einen Vornamen ein:");
            var firstname = Console.ReadLine();
            Console.WriteLine("Bitte geben Sie einen Nachnamen ein:");
            var lastname = Console.ReadLine();
            var user = new User(firstname, lastname);
            var changing = _datenhaltung.UpdateUser(user);            
        }

        public void UserDelete()
        {
            Console.WriteLine("Zum entfernen eines Users werden einige Daten benötigt:");
            Console.WriteLine("Bitte geben Sie einen Vornamen ein:");
            var firstname = Console.ReadLine();
            Console.WriteLine("Bitte geben Sie einen Nachnamen ein:");
            var lastname = Console.ReadLine();
            var user = new User(firstname, lastname);

            if (_fachkonzept.deleteUser(user) == true)
            {
                Console.WriteLine("Der ausgewählte User wurde entfernt.");
                MainMenu();
            }
            else
            {
                Console.WriteLine("Der ausgewählte User konnte nicht entfernt werden.");
                MainMenu();
            }

        }
        #endregion

        #region Game Functions

        public void GameDisplay()
        {
            Console.WriteLine("Der Display mehrerer Spiele ist nicht Implementiert:");
            var gameList = _datenhaltung.GetGame();
            foreach (var game in gameList)
            {
                Console.WriteLine("Die ausgewählten Userdaten:");
                Console.WriteLine(game.Name + "\r" + game.Game_id + "\n");
            }
            Console.WriteLine("Beenden \t \t (x)");
            var end = Convert.ToChar(Console.ReadLine());
            if (end == 'x')
                MainMenu();
        }

        public void GameSearch()
        {
            Console.WriteLine("Zum suchen nach einem Spiel wird der Name benötigt:");
            var name = Console.ReadLine();
            var gameList = _fachkonzept.searchGame(name);

            foreach (var game in gameList)
            {
                Console.WriteLine("Die ausgewählten Userdaten:");
                Console.WriteLine(game.Name + "\r" + game.Game_id);
            }
            Console.WriteLine("Beenden \t \t (x)");
            var end = Convert.ToChar(Console.ReadLine());
        }

        public void GameAdd()
        {
            Console.WriteLine("Zum hinzufügen eines Spiels wird der Name benötigt:");
            var name = Console.ReadLine();
            var game = new Game(name);

            if (_fachkonzept.createGame(game) == true)
            {
                Console.WriteLine("Das ausgewählte Spiel wurde erstellt.");
                MainMenu();
            }
            else
            {
                Console.WriteLine("Das ausgewählte Spiel konnte nicht erstellt werden.");
                MainMenu();
            }
        }

        public void GameChange()
        {
            Console.WriteLine("Bitte geben Sie einen Namen ein:");
            var name = Console.ReadLine();
            var game = new Game(name);

            var changing = _datenhaltung.UpdateGame(game);            
        }

        public void GameDelete()
        {
            Console.WriteLine("Zum entfernen eines Spiels wird der Name benötigt:");
            var name = Console.ReadLine();
            var game = new Game(name);

            if (_fachkonzept.deleteGame(game) == true)
            {
                Console.WriteLine("Das ausgewählte Spiel wurde entfernt.");
                MainMenu();
            }
            else
            {
                Console.WriteLine("Das ausgewählte Spiel konnte nicht entfernt werden.");
                MainMenu();
            }
        }
        #endregion

        #region Relationship functions
        
        public void RelationAdd()
        {
            Console.WriteLine("Zum hinzufügen einer Bezeihung werden die eindeutigen IDs benötigt:");
            Console.WriteLine("Bitte geben Sie die User ID an:");
            var userID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Bitte geben Sie die Spiele ID an:");
            var gameID = Convert.ToInt32(Console.ReadLine());
            var name = Console.ReadLine();
            var game = new Game(name);
            var link = _datenhaltung.AddLink(userID, gameID);
        }

        public void RelationCut()
        {
            Console.WriteLine("Zum entfernen einer Bezeihung werden die eindeutigen IDs benötigt:");
            Console.WriteLine("Bitte geben Sie die User ID an:");
            var userID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Bitte geben Sie die Spiele ID an:");
            var gameID = Convert.ToInt32(Console.ReadLine());
            var name = Console.ReadLine();
            var game = new Game(name);
            var link = _datenhaltung.DeleteLink(userID, gameID);
        }
        #endregion
    }
}
