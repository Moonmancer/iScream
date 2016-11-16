using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace iScream
{
    class Datenhaltung2 : IDatenhaltung
    {
        private XMLDatabase database;

        public Datenhaltung2()
        {
            database = new XMLDatabase();
            database.Load();
        }

        #region Holen
        public int GetNextUser_id()
        {
            if (database.UserData.Count > 0)
                return database.UserData.Last().User_id + 1;
            else
                return 1;
        }

        public int GetNextGame_id()
        {
            if (database.GameData.Count > 0)
                return database.GameData.Last().Game_id + 1;
            else
                return 1;
        }

        public User GetUser(int user_id)
        {
            return database.UserData.Find(x => x.User_id == user_id);
        }

        public List<User> GetUser()
        {
            return database.UserData;
        }

<<<<<<< HEAD
        public User HoleNutzer(int nutzer_id)
=======
        public Game GetGame(int game_id)
>>>>>>> refs/remotes/origin/Datenhaltung
        {
            return database.GameData.Find(x => x.Game_id == game_id);
        }

<<<<<<< HEAD
        public List<User> HoleNutzer()
=======
        public List<Game> GetGame()
>>>>>>> refs/remotes/origin/Datenhaltung
        {
            return database.GameData;
        }

<<<<<<< HEAD
        public Game HoleSpiel(int spiel_id)
=======
        public Link GetLink(int user_id, int game_id)
>>>>>>> refs/remotes/origin/Datenhaltung
        {
            throw new NotImplementedException();
        }

<<<<<<< HEAD
        public List<Game> HoleSpiel()
=======
        public Link GetLink(Link link)
>>>>>>> refs/remotes/origin/Datenhaltung
        {
            throw new NotImplementedException();
        }

<<<<<<< HEAD
        public List<Link> HoleVerknüpfung()
=======
        public List<Link> GetLink()
>>>>>>> refs/remotes/origin/Datenhaltung
        {
            return database.Links;
        }

<<<<<<< HEAD
        public List<Game> HoleSpieleVonNutzer(int nutzer_id)
        {
            List<Game> result = new List<Game>();
            List<Link> tmp = database.Verknüpfungen.FindAll(x => x.Nutzer_id == nutzer_id);
            foreach (Link verknüpfung in tmp)
                result.Add(HoleSpiel(verknüpfung.Spiel_id));
            return result;
        }
        #region HoleSpieleVonNutzer-Overloads
        public List<Game> HoleSpieleVonNutzer(User nutzer)
=======
        public List<Game> GetGamesOfUser(int user_id)
        {
            List<Game> result = new List<Game>();
            List<Link> tmp = database.Links.FindAll(x => x.User_id == user_id);
            foreach (Link link in tmp)
                result.Add(GetGame(link.Game_id));
            return result;
        }
        #region HoleSpieleVonNutzer-Overloads
        public List<Game> GetGamesOfUser(User user)
>>>>>>> refs/remotes/origin/Datenhaltung
        {
            return GetGamesOfUser(user.User_id);
        }
        #endregion

<<<<<<< HEAD
        public List<User> HoleNutzerVonSpiel(int spiel_id)
        {
            List<User> result = new List<User>();
            foreach (Link verknüpfung in database.Verknüpfungen.FindAll(x => x.Spiel_id == spiel_id))
                result.Add(HoleNutzer(verknüpfung.Nutzer_id));
            return result;
        }
        #region HoleNutzerVonSpiel-Overloads
        public List<User> HoleNutzerVonSpiel(Game spiel)
=======
        public List<User> GetUserOfGame(int game_id)
        {
            List<User> result = new List<User>();
            foreach (Link link in database.Links.FindAll(x => x.Game_id == game_id))
                result.Add(GetUser(link.User_id));
            return result;
        }
        #region HoleNutzerVonSpiel-Overloads
        public List<User> GetUserOfGame(Game spiel)
>>>>>>> refs/remotes/origin/Datenhaltung
        {
            return GetUserOfGame(spiel.Game_id);
        }
        #endregion
        #endregion

        #region Hinzufügen
        public bool AddUser(string vorname, string nachname, int user_id)
        {
            return AddUser(new User(vorname, nachname, user_id));
        }

        public bool AddUser(string vorname, string nachname)
        {
<<<<<<< HEAD
            return FügeNutzerHinzu(new User(vorname, nachname, id));
        }

        public bool FügeNutzerHinzu(User nutzer)
=======
            return AddUser(new User(vorname, nachname, GetNextUser_id()));
        }

        public bool AddUser(User user)
>>>>>>> refs/remotes/origin/Datenhaltung
        {
            if (user.User_id == -1)
                user.User_id = GetNextUser_id();

            if (!database.UserData.Exists(x => x.User_id == user.User_id))
            {
                database.UserData.Add(user);
                database.Save();
                return true;
            }
            else
                return false;
        }

<<<<<<< HEAD
        public void FügeNutzerHinzu(List<User> nutzer)
        {
            foreach (User cur in nutzer)
                FügeNutzerHinzu(cur);
=======
        public void AddUser(List<User> user)
        {
            foreach (User cur in user)
                AddUser(cur);
        }

        public bool AddGame(string name, int id)
        {
            return AddGame(new Game(name, id));
>>>>>>> refs/remotes/origin/Datenhaltung
        }

        public bool AddGame(string name)
        {
<<<<<<< HEAD
            return FügeSpielHinzu(new Game(name, id));
        }

        public bool FügeSpielHinzu(Game spiel)
=======
            return AddGame(new Game(name, GetNextGame_id()));
        }

        public bool AddGame(Game spiel)
>>>>>>> refs/remotes/origin/Datenhaltung
        {
            if (spiel.Game_id == -1)
                spiel.Game_id = GetNextGame_id();

            if (!database.GameData.Exists(x => x.Game_id == spiel.Game_id))
            {
                database.GameData.Add(spiel);
                database.Save();
                return true;
            }
            else
                return false;

        }

<<<<<<< HEAD
        public void FügeSpielHinzu(List<Game> spiele)
        {
            foreach (Game cur in spiele)
                FügeSpielHinzu(cur);
=======
        public void AddGame(List<Game> spiele)
        {
            foreach (Game cur in spiele)
                AddGame(cur);
>>>>>>> refs/remotes/origin/Datenhaltung
        }

        public bool AddLink(int user_id, int game_id)
        {
<<<<<<< HEAD
            return FügeVerknüpfungHinzu(new Link(nutzer_id, spiel_id));
        }

        public bool FügeVerknüpfungHinzu(Link verknüpfung)
=======
            return AddLink(new Link(user_id, game_id));
        }

        public bool AddLink(Link link)
>>>>>>> refs/remotes/origin/Datenhaltung
        {
            if (!database.Links.Exists(x => x.User_id == link.User_id && x.Game_id == link.Game_id))
            {
                database.Links.Add(link);
                database.Save();
                return true;
            }
            else
                return false;
        }

<<<<<<< HEAD
        public void FügeVerknüpfungHinzu(List<Link> verknüpfungen)
        {
            foreach (Link cur in verknüpfungen)
                FügeVerknüpfungHinzu(cur.Nutzer_id, cur.Spiel_id);
=======
        public void AddLink(List<Link> links)
        {
            foreach (Link cur in links)
                AddLink(cur);
>>>>>>> refs/remotes/origin/Datenhaltung
        }
        #endregion

        #region Löschen
        public bool DeleteUser(int user_id)
        {
            return DeleteUser(database.UserData.Find(x => x.User_id == user_id));
        }

        public bool DeleteUser(User user)
        {
            if (database.UserData.Remove(user))
            {
                foreach (Link cur in database.Links.FindAll(x => x.User_id == user.User_id))
                    DeleteLink(cur);
                database.Save();

                return true;
            }
            else
                return false;
        }
        public void DeleteUser(List<User> user)
        {
            foreach (User cur in user)
                DeleteUser(cur);
        }

        public bool DeleteGame(int game_id)
        {
            return DeleteGame(database.GameData.Find(x => x.Game_id == game_id));
        }
        public bool DeleteGame(Game spiel)
        {
            if (database.GameData.Remove(spiel))
            {
                foreach (Link cur in database.Links.FindAll(x => x.Game_id == spiel.Game_id))
                    DeleteLink(cur);
                database.Save();

                return true;
            }
            else
                return false;
        }
        public void DeleteGame(List<Game> spiele)
        {
            foreach (Game cur in spiele)
                DeleteGame(cur);
        }

        public bool DeleteLink(int user_id, int game_id)
        {
            return DeleteLink(database.Links.Find(x => x.User_id == user_id && x.Game_id == game_id));
        }
        public bool DeleteLink(Link link)
        {
            if (database.Links.Remove(link))
            {
                database.Save();

                return true;
            }
            else
                return false;

        }
        #endregion

        #region Ändern
        public bool UpdateUser(int user_id, string vorname, string nachname)
        {
            try
            {
                database.UserData.Where(x => x.User_id == user_id).First().Firstname = vorname;
                database.UserData.Where(x => x.User_id == user_id).First().Lastname = nachname;
                database.Save();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateUser(User user)
        {
            return UpdateUser(user.User_id, user.Firstname, user.Lastname);
        }

        public bool UpdateGame(int game_id, string name)
        {
            try
            {
                database.GameData.Where(x => x.Game_id == game_id).First().Name = name;
                database.Save();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateGame(Game game)
        {
            return UpdateGame(game.Game_id, game.Name);
        }
        #endregion

        #region Suchen
        public List<User> SearchUser(string firstname, string lastname)
        {
            if (String.IsNullOrEmpty(firstname) && String.IsNullOrEmpty(lastname))
                return GetUser();
            else if (String.IsNullOrEmpty(firstname))
                return database.UserData.FindAll(x => x.Lastname.Contains(lastname));
            else if (String.IsNullOrEmpty(lastname))
                return database.UserData.FindAll(x => x.Lastname.Contains(firstname));
            else
                return database.UserData.FindAll(x => x.Firstname.Contains(firstname) && x.Lastname.Contains(lastname));
        }

        public List<Game> SearchGame(string name)
        {
            if (String.IsNullOrEmpty(name))
                return GetGame();
            else
                return database.GameData.FindAll(x => x.Name.Contains(name));
        }
        #endregion
    }

    public class XMLDatabase
    {
        private static string FILEPATH = Settings.CurrentSettings.XmlDatabaseLocation;

<<<<<<< HEAD
        private List<User> nutzerDaten;
        public List<User> NutzerDaten
=======
        private List<User> userData;
        public List<User> UserData
>>>>>>> refs/remotes/origin/Datenhaltung
        {
            get { return userData; }
            set { userData = value; }
        }

<<<<<<< HEAD
        private List<Game> spielDaten;
        public List<Game> SpielDaten
=======
        private List<Game> gameData;
        public List<Game> GameData
>>>>>>> refs/remotes/origin/Datenhaltung
        {
            get { return gameData; }
            set { gameData = value; }
        }

<<<<<<< HEAD
        private List<Link> verknüpfungen;
        public List<Link> Verknüpfungen
=======
        private List<Link> links;
        public List<Link> Links
>>>>>>> refs/remotes/origin/Datenhaltung
        {
            get { return links; }
            set { links = value; }
        }

        public XMLDatabase()
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Settings.CurrentSettings.XmlDatabaseLocation));

<<<<<<< HEAD
            nutzerDaten = new List<User>();
            spielDaten = new List<Game>();
            verknüpfungen = new List<Link>();
=======
            userData = new List<User>();
            gameData = new List<Game>();
            links = new List<Link>();
>>>>>>> refs/remotes/origin/Datenhaltung
        }

        public bool Load()
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(FILEPATH, System.IO.FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(XMLDatabase));
                    XMLDatabase tmp = (XMLDatabase)xs.Deserialize(fs);

                    this.userData = tmp.userData;
                    this.gameData = tmp.gameData;
                    this.links = tmp.links;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Save()
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(FILEPATH, System.IO.FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(XMLDatabase));
                xs.Serialize(fs, this);
            }
        }
    }
}
