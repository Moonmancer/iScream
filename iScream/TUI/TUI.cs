using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream.TUI
{
    class TUI
    {
        private IFachkonzept _fachkonzept;

        public TUI(IFachkonzept fachkonzept)
        {
            _fachkonzept = fachkonzept;
            MainMenu();
        }

        public void Run()
        {
            MainMenu();
        }
        
        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine(" Hauptmenu: \r\n Bitte Wählen Sie eine Funktion aus:\r\n");
            Console.WriteLine(" --------------------------------------");
            Console.WriteLine(" Nutzer anzeigen \t \t (a)");
            Console.WriteLine(" Nutzer suchen \t \t \t (b)");
            Console.WriteLine(" Nutzer hinzufügen \t \t (c)");
            Console.WriteLine(" Nutzer bearbeiten \t \t (d)");
            Console.WriteLine(" Nutzer löschen \t \t (e)");
            Console.WriteLine(" --------------------------------------");
            Console.WriteLine(" Spiele anzeigen \t \t (f)");
            Console.WriteLine(" Spiel suchen \t \t \t (g)");
            Console.WriteLine(" Spiel hinzufügen \t \t (h)");
            Console.WriteLine(" Spiel bearbeiten \t \t (i)");
            Console.WriteLine(" Spiel löschen \t \t \t (j)");
            Console.WriteLine(" --------------------------------------");
            Console.WriteLine(" Beziehung Anzeigen \t \t (k)");
            Console.WriteLine(" Beziehung hinzufügen \t \t (l)");
            Console.WriteLine(" Beziehung löschen \t \t (m)");
            Console.WriteLine(" Beenden \t \t \t (x)");

            Console.WindowWidth = 65;

            try
            {
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
                    RelationDisplay();
                    break;
                case 'l':
                    Console.Clear();
                    RelationAdd();
                    break;
                case 'm':
                    Console.Clear();
                    RelationCut();
                    break;
                case 'x':
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                default:
                    Console.Write("Bitte tätigen Sie eine valide Eingabe");
                    Console.ReadKey();
                    break;
            }

            }
            catch (Exception ex)
            {

            }
        }

        #region User Functions

        public void UserDisplay()
        {
            Console.WriteLine("Darstellung aller User:");
            var userList = _fachkonzept.getUsers();

            Console.WriteLine("Die ausgewählten Userdaten:");
            foreach (var user in userList)
            {
                Console.WriteLine("User Nachname: " + user.Lastname + ", User Vorname: " + user.Firstname + ", User ID: " + user.User_id + "\n");
            }
            Console.WriteLine("Beenden \t \t (x)");
            
            var end = Convert.ToChar(Console.ReadLine());
            if (end == 'x')
            {
                MainMenu();
            }

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
                Console.WriteLine("User Name: " + user.Name + ", User ID: " + user.User_id + "\n");
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
            Console.WriteLine("Zum ändern von User Daten wird benötigt:");
            Console.WriteLine("Bitte geben Sie einen Vornamen ein:");
            var firstname = Console.ReadLine();
            Console.WriteLine("Bitte geben Sie einen Nachnamen ein:");
            var lastname = Console.ReadLine();
            var user = new User(firstname, lastname);
            var changing = _fachkonzept.updateUser(user);            
        }

        public void UserDelete()
        {
            //Console.WriteLine("Zum entfernen eines Users werden einige Daten benötigt:");
            //Console.WriteLine("Bitte geben Sie einen Vornamen ein:");
            //var firstname = Console.ReadLine();
            //Console.WriteLine("Bitte geben Sie einen Nachnamen ein:");
            //var lastname = Console.ReadLine();
            //var user = new User(firstname, lastname);
            Console.WriteLine("Zum entfernen eines Users wird die User-ID benötigt:");
            Console.WriteLine("Bitte geben Sie die UserID ein:");
            var userid = Convert.ToInt32(Console.ReadLine());


            if (_fachkonzept.deleteUser(userid) == true)
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
            Console.WriteLine("Darstellung aller Spiele:");
            var gameList = _fachkonzept.getGames();
            Console.WriteLine("Die ausgewählten Userdaten:");
            foreach (var game in gameList)
            {
                Console.WriteLine("Spiel Name: " + game.Name + ", Spiel ID: " + game.Game_id + "\n");
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
                Console.WriteLine("Spiel Name: " + game.Name + ", Spiel ID: " + game.Game_id);
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
            Console.WriteLine("Zum ändern von Spiel Daten wird der Name benötigt:");
            Console.WriteLine("Bitte geben Sie einen Namen ein:");
            var name = Console.ReadLine();
            var game = new Game(name);

            var changing = _fachkonzept.updateGame(game);            
        }

        public void GameDelete()
        {
            //Console.WriteLine("Zum entfernen eines Spiels wird der Name benötigt:");
            //var name = Console.ReadLine();
            //var game = new Game(name);
            Console.WriteLine("Zum entfernen eines Spiels wird die ID benötigt:");
            var gameid = Convert.ToInt32(Console.ReadLine());

            if (_fachkonzept.deleteGame(gameid) == true)
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
            try
            {
                Console.WriteLine("Zum hinzufügen einer Bezeihung werden die eindeutigen IDs benötigt:");
                Console.WriteLine("Bitte geben Sie die User ID an:");
                var userID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Bitte geben Sie die Spiele ID an:");
                var gameID = Convert.ToInt32(Console.ReadLine());
                var link = new Link(userID, gameID);
                _fachkonzept.createLink(link);
            }
            catch(Exception)
            {
                Console.WriteLine("Beim erstellen der Beziehung ist ein fehler unterlaufen.");
                MainMenu();
            }
        }

        public void RelationCut()
        {
            try
            {
                Console.WriteLine("Zum entfernen einer Bezeihung werden die eindeutigen IDs benötigt:");
                Console.WriteLine("Bitte geben Sie die User ID an:");
                var userID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Bitte geben Sie die Spiele ID an:");
                var gameID = Convert.ToInt32(Console.ReadLine());
                _fachkonzept.deleteLink(userID, gameID);
            }
            catch (Exception)
            {
                Console.WriteLine("Beim entfernen der Beziehung ist ein fehler unterlaufen.");
                MainMenu();
            }
        }

        public void RelationDisplay()
        {
            try
            {
                Console.WriteLine("Darstellung aller Beziehungen:");
                var linkList = _fachkonzept.getLinks();
                Console.WriteLine("Die ausgewählten Beziehungen:");
                foreach (var link in linkList)
                {
                    Console.WriteLine("Spiel ID: " + link.Game_id + ", User ID: " + link.User_id + "\n");
                }
                Console.WriteLine("Beenden \t \t (x)");
                var end = Convert.ToChar(Console.ReadLine());
                if (end == 'x')
                    MainMenu();
            }
            catch (Exception)
            {
                Console.WriteLine("Beim anzeigen der Beziehungen ist ein fehler unterlaufen.");
                MainMenu();
            }
        }
        #endregion
    }
}
