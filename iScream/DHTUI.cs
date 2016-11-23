using System;
using System.Collections.Generic;

namespace iScream
{
    class DHTUI
    {
        private IDatenhaltung dh;

        public DHTUI(IDatenhaltung dh)
        {
            this.dh = dh;
        }

        public void Run()
        {
            //Console.Clear();

            ShowMainMenu();
        }

        private char GetInput(List<char> allowedChars)
        {
            char result = Console.ReadKey().KeyChar;
            while (!allowedChars.Contains(result))
                result = Console.ReadKey().KeyChar;
            Console.WriteLine();
            return result;
        }

        private string GetInputBlacklist(List<string> blacklist)
        {
            string result = Console.ReadLine();
            while (blacklist.Contains(result))
                result = Console.ReadLine();

            return result;
        }

        private string GetInput(List<string> allowedStrings)
        {
            string result = Console.ReadLine();
            while (!allowedStrings.Contains(result))
                result = Console.ReadLine();

            return result;
        }

        private void UserDetails(User user)
        {
            if (user == null)
                return;
            Console.WriteLine("\nNutzer " + user.User_id + ":");
            Console.WriteLine("\tVorname:\t" + user.Firstname);
            Console.WriteLine("\tNachname:\t" + user.Lastname);

            string games = "";
            foreach (Game game in dh.GetGamesOfUser(user))
                if (games.Length == 0)
                    games = game.Name;
                else
                    games += ", " + game.Name;

            if (games.Length > 0)
                Console.WriteLine("\tSpiele:\t" + games);
        }

        private void GameDetails(Game game)
        {
            Console.WriteLine("\nSpiel " + game.Game_id + ":");
            Console.WriteLine("\tName:\t" + game.Name);

            List<User> usersOfGame = dh.GetUserOfGame(game);
            string user = "";

            foreach (User cur in dh.GetUserOfGame(game))
            {
                if (user != "")
                    user += ", ";
                user += cur.Name + " (" + cur.User_id + ")";
            }

            if (user.Length > 0)
                Console.WriteLine("\tNutzer:\t" + user);
        }

        private List<string> AllUsers()
        {
            Console.WriteLine("\nAlle Nutzer:");
            List<string> allowedStrings = new List<string>();
            foreach (User user in dh.GetUser())
            {
                allowedStrings.Add(user.User_id.ToString());
                Console.WriteLine("\t" + user.User_id + "\t" + user.Name);
            }

            return allowedStrings;
        }

        private void ShowUserOfGame(Game game)
        {
            List<User> user = dh.GetUserOfGame(game);

            if (user != null)
            {
                Console.WriteLine("\nNutzer von \"" + game.Name + "\":");
                foreach (User cur in user)
                    Console.WriteLine("\t" + cur.User_id + "\t" + cur.Name);
            }
        }

        private void ShowGamesOfUser(int userid)
        {
            User user = dh.GetUser(userid);

            List<Game> games = dh.GetGamesOfUser(userid);

            if (games != null)
            {
                Console.WriteLine("\nSpiele von " + user.Name + ":");
                foreach (Game cur in games)
                    Console.WriteLine("\t" + cur.Game_id + "\t" + cur.Name);
            }
        }

        private List<string> SearchUser(string firstname, string lastname)
        {
            Console.WriteLine("\nSuchergebnis (\"" + firstname + "\",\"" + lastname + "\"):");
            List<string> allowedStrings = new List<string>();
            foreach (User user in dh.SearchUser(firstname, lastname))
            {
                allowedStrings.Add(user.User_id.ToString());
                Console.WriteLine("\t" + user.User_id + "\t" + user.Name);
            }

            return allowedStrings;
        }

        private List<string> AllGames()
        {
            Console.WriteLine("\nAlle Spiele:");
            List<string> allowedStrings = new List<string>();
            foreach (Game game in dh.GetGame())
            {
                allowedStrings.Add(game.Game_id.ToString());
                Console.WriteLine("\t" + game.Game_id + "\t" + game.Name);
            }

            return allowedStrings;
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("Hauptmenü");
            Console.WriteLine("1) Alle Nutzer anzeigen");
            Console.WriteLine("2) Alle Spiele anzeigen");
            Console.WriteLine("3) Nutzer anzeigen");
            Console.WriteLine("4) Spiel anzeigen");
            Console.WriteLine("5) Spiele von Nutzer anzeigen");
            Console.WriteLine("6) Nutzer von Spiele anzeigen");
            Console.WriteLine("7) Hinzufügen");
            Console.WriteLine("8) Löschen");
            Console.WriteLine("9) Ändern");
            Console.WriteLine("q) Beenden");

            char input = GetInput(new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'q' });

            switch (input)
            {
                case '1':
                    AllUsers();
                    break;
                case '2':
                    AllGames();
                    break;
                case '3':
                    ShowShowUserMenu();
                    break;
                case '4':
                    ShowShowGameMenu();
                    break;
                case '5':
                    ShowShowGamesOfUserMenu();
                    break;
                case '6':
                    ShowShowUserOfGamesMenu();
                    break;
                case '7':
                    ShowAddMenu();
                    break;
                case '8':
                    ShowDeleteMenu();
                    break;
                case '9':
                    ShowUpdateMenu();
                    break;
                case 'q':
                    break;
            }

            if (input != 'q')
            {
                Console.WriteLine("\nEingabetaste drücken um zum Hauptmenü zurückzukehren");
                Console.ReadLine();
                ShowMainMenu();
            }
        }

        private void ShowShowUserMenu()
        {
            Console.WriteLine("\nNutzer anzeigen:");
            int userid = ShowUserSubMenu();
            if (userid != -1)
                UserDetails(dh.GetUser(userid));
        }

        private void ShowShowGamesOfUserMenu()
        {
            Console.WriteLine("Spiele von Nutzer anzeigen:");
            int userid = ShowUserSubMenu();
            if (userid != -1)
                ShowGamesOfUser(userid);
        }

        private int ShowUserSubMenu()
        {
            Console.WriteLine("1) Nutzer ID angeben");
            Console.WriteLine("2) Nutzer suchen");
            Console.WriteLine("q) Zum Hauptmenü");

            bool askForUserID = false;
            List<string> allowedStrings = new List<string>();

            switch (GetInput(new List<char> { '1', '2', 'q' }))
            {
                case '1':
                    askForUserID = true;
                    break;
                case '2':
                    Console.WriteLine("Vorname angeben (\"\" = nicht nach Vorname suchen):");
                    string firstname = Console.ReadLine();
                    Console.WriteLine("Nachname angeben (\"\" = nicht nach Nachname suchen):");
                    string lastname = Console.ReadLine();
                    if (String.IsNullOrEmpty(firstname) && String.IsNullOrEmpty(lastname))
                        Console.WriteLine("Keinen Suchbegriff angegeben!");
                    else
                    {
                        allowedStrings = SearchUser(firstname, lastname);
                        askForUserID = true;
                    }
                    break;
                case 'q':
                    break;
            }

            if (askForUserID)
            {
                if (allowedStrings.Count == 0)
                    foreach (User user in dh.GetUser())
                        allowedStrings.Add(user.User_id.ToString());

                allowedStrings.Add("q");

                Console.WriteLine("Nutzer ID angeben:");
                string input = GetInput(allowedStrings);
                while (!allowedStrings.Contains(input))
                    if (input == "q")
                        return -1;
                    else
                        input = GetInput(allowedStrings);
                int userid = -1;
                int.TryParse(input, out userid);
                return userid;
            }
            else
                return -1;
        }

        private void ShowShowGameMenu()
        {
            Console.WriteLine("\nSpiel anzeigen:");
            Game game = ShowGameSubMenu();
            if (game != null)
                GameDetails(game);
        }

        private Game ShowGameSubMenu()
        {
            Console.WriteLine("1) Spiel ID angeben");
            Console.WriteLine("2) Name angeben");
            Console.WriteLine("q) Zum Hauptmenü");

            char input = GetInput(new List<char> { '1', '2', 'q' });

            List<Game> games = dh.GetGame();
            List<string> gameNames = new List<string>();
            List<string> gameIDs = new List<string>();
            foreach (Game game in games)
            {
                gameIDs.Add(game.Game_id.ToString());
                gameNames.Add(game.Name);
            }

            gameIDs.Add("q");
            gameNames.Add("q");

            switch (input)
            {
                case '1':
                    Console.WriteLine("Spiel ID angeben:");
                    int gameid = -1;
                    string IDinput = GetInput(gameIDs);
                    if (IDinput != "q")
                        int.TryParse(IDinput, out gameid);
                    if (gameid <= 0)
                        return null;
                    return dh.GetGame(gameid);
                case '2':
                    Console.WriteLine("Name angeben:");
                    string name = GetInput(gameNames);
                    if (String.IsNullOrEmpty(name) || name == "q")
                        return null;
                    else
                        return dh.GetGame(name);
                case 'q':
                    break;
            }

            return null;
        }

        private void ShowShowUserOfGamesMenu()
        {
            Console.WriteLine("\nNutzer von Spiel anzeigen:");
            Game game = ShowGameSubMenu();
            if (game != null)
                ShowUserOfGame(game);
        }

        private void ShowAddUserMenu()
        {
            Console.WriteLine("\nNutzer hinzufügen:");
            Console.WriteLine("Vorname angeben:");
            string firstname = GetInputBlacklist(new List<string> { "" });
            if (firstname == "q")
                return;
            Console.WriteLine("Nachname angeben:");
            string lastname = GetInputBlacklist(new List<string> { "" });
            if (lastname == "q")
                return;
            if (dh.AddUser(firstname, lastname))
                Console.WriteLine("Nutzer " + firstname + " " + lastname + " erfolgreich hinzugefügt!");
            else
                Console.WriteLine("Nutzer " + firstname + " " + lastname + " wurde NICHT hinzugefügt!");
        }

        private void ShowAddGameMenu()
        {
            Console.WriteLine("\nSpiel hinzufügen:");
            Console.WriteLine("Name angeben:");
            string name = GetInputBlacklist(new List<string> { "" });
            if (name == "q")
                return;
            if (dh.AddGame(name))
                Console.WriteLine("Spiel erfolgreich hinzugefügt!");
            else
                Console.WriteLine("Spiel wurde NICHT hinzugefügt!");
        }

        private void ShowAddLinkMenu()
        {
            Console.WriteLine("\nVerknüpfung hinzufügen:");
            Console.WriteLine("Nutzer angeben:");
            int userid = ShowUserSubMenu();
            if (userid == -1)
                return;

            Console.WriteLine("Spiel angeben:");
            Game game = ShowGameSubMenu();
            if (game != null)
                if (dh.AddLink(userid, game.Game_id))
                    Console.WriteLine("Verknüpfung zwischen " + dh.GetUser(userid) + " und \"" + game.Name + "\" hinzugefügt!");
                else
                    Console.WriteLine("Verknüpfung zwischen " + dh.GetUser(userid) + " und \"" + game.Name + "\" NICHT hinzugefügt!");
        }

        private void ShowAddMenu()
        {
            Console.WriteLine("\nHinzufügen:");
            Console.WriteLine("1) Nutzer hinzufügen");
            Console.WriteLine("2) Spiel hinzufügen");
            Console.WriteLine("3) Verknüpfung hinzufügen");
            Console.WriteLine("q) Zum Hauptmenü");

            char input = GetInput(new List<char> { '1', '2', '3', 'q' });

            switch (input)
            {
                case '1':
                    Console.WriteLine("Nutzer hinzufügen:");
                    ShowAddUserMenu();
                    break;
                case '2':
                    Console.WriteLine("Spiel hinzufügen:");
                    ShowAddGameMenu();
                    break;
                case '3':
                    Console.WriteLine("Verknüpfung hinzufügen:");
                    ShowAddLinkMenu();
                    break;
                case 'q':
                    break;
            }
        }

        private void ShowDeleteMenu()
        {
            Console.WriteLine("\nLöschen:");
            Console.WriteLine("1) Nutzer löschen");
            Console.WriteLine("2) Spiel löschen");
            Console.WriteLine("3) Verknüpfung löschen");
            Console.WriteLine("q) Zum Hauptmenü");

            char input = GetInput(new List<char> { '1', '2', '3', 'q' });
            int userid;
            Game game;
            switch (input)
            {
                case '1':
                    Console.WriteLine("Nutzer löschen:");
                    userid = ShowUserSubMenu();
                    if (userid > 0)
                    {
                        User user = dh.GetUser(userid);
                        if (dh.DeleteUser(userid))
                            Console.WriteLine("Nutzer " + user.Name + " (" + userid + ") erfolgreich gelöscht!");
                        else
                            Console.WriteLine("Nutzer " + user.Name + " (" + userid + ") NICHT gelöscht!");
                    }
                    break;
                case '2':
                    Console.WriteLine("Spiel löschen:");
                    game = ShowGameSubMenu();
                    if (game != null)
                        if (dh.DeleteGame(game))
                            Console.WriteLine("Spiel \"" + game.Name + "\" (" + game.Game_id + ") erfolgreich gelöscht!");
                        else
                            Console.WriteLine("Spiel \"" + game.Name + "\" (" + game.Game_id + ") NICHT gelöscht!");
                    break;
                case '3':
                    Console.WriteLine("Verknüpfung löschen:");
                    Console.WriteLine("Nutzer ID angeben:");
                    List<string> allowedStrings = new List<string>();
                    foreach (User user in dh.GetUser())
                        allowedStrings.Add(user.User_id.ToString());
                    userid = -1;
                    int.TryParse(GetInput(allowedStrings), out userid);
                    if (userid == -1)
                        return;

                    ShowGamesOfUser(userid);

                    Console.WriteLine("Spiel ID angeben:");
                    allowedStrings = new List<string>();
                    foreach (Game cur in dh.GetGamesOfUser(userid))
                        allowedStrings.Add(cur.Game_id.ToString());
                    int gameid = -1;
                    int.TryParse(GetInput(allowedStrings), out gameid);
                    if (gameid != -1)
                        if (dh.DeleteLink(userid, gameid))
                            Console.WriteLine("Verknüpfung zwischen Nutzer " + dh.GetUser(userid).Name + " (" + userid + ") und Spiel \"" + dh.GetGame(gameid).Name + "\" (" + gameid + ") erfolgreich gelöscht!");
                        else
                            Console.WriteLine("Verknüpfung zwischen Nutzer " + dh.GetUser(userid).Name + " (" + userid + ") und Spiel \"" + dh.GetGame(gameid).Name + "\" (" + gameid + ") NICHT gelöscht!");
                    break;
                case 'q':
                    break;
            }
        }

        private void ShowUpdateMenu()
        {
            Console.WriteLine("\nÄndern:");
            Console.WriteLine("1) Nutzer ändern");
            Console.WriteLine("2) Spiel ändern");
            Console.WriteLine("q) Zum Hauptmenü");

            char input = GetInput(new List<char> { '1', '2', 'q' });

            switch (input)
            {
                case '1':
                    Console.WriteLine("Nutzer ändern:");
                    int userid = ShowUserSubMenu();
                    User user = dh.GetUser(userid);
                    UserDetails(user);
                    Console.WriteLine("Neuen Vornamen eingeben (aktuell: " + user.Firstname + ", \"\" = keine Änderung):");
                    string firstname = Console.ReadLine();
                    Console.WriteLine("Neuen Nachnamen eingeben (aktuell: " + user.Lastname + ", \"\" = keine Änderung):");
                    string lastname = Console.ReadLine();
                    if (dh.UpdateUser(userid, firstname, lastname))
                        Console.WriteLine("Benutzer erfolgreich geändert!");
                    else
                        Console.WriteLine("Benutzer NICHT geändert!");
                    break;
                case '2':
                    Console.WriteLine("Spiel ändern:");
                    Game game = ShowGameSubMenu();
                    GameDetails(game);
                    Console.WriteLine("Neuen Namen eingeben (aktuell: \"" + game.Name + "\", \"\" = keine Änderung):");
                    string name = Console.ReadLine();
                    if (dh.UpdateGame(game.Game_id, name))
                        Console.WriteLine("Spiel erfolgreich geändert!");
                    else
                        Console.WriteLine("Spiel NICHT geändert!");
                    break;
                case 'q':
                    break;
            }
        }
    }
}