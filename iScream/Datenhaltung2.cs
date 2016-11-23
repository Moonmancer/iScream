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

        #region Get
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

        public Game GetGame(int game_id)
        {
            return database.GameData.Find(x => x.Game_id == game_id);
        }

        public Game GetGame(string name)
        {
            return database.GameData.Find(x => x.Name == name);
        }

        public List<Game> GetGame()
        {
            return database.GameData;
        }

        public Link GetLink(int user_id, int game_id)
        {
            return database.Links.Find(x => x.User_id == user_id && x.Game_id == game_id);
        }

        public Link GetLink(Link link)
        {
            return GetLink(link.User_id, link.Game_id);
        }

        public List<Link> GetLink()
        {
            return database.Links;
        }

        public List<Game> GetGamesOfUser(int user_id)
        {
            List<Game> result = new List<Game>();
            List<Link> tmp = database.Links.FindAll(x => x.User_id == user_id);
            foreach (Link link in tmp)
                result.Add(GetGame(link.Game_id));
            return result;
        }

        public List<Game> GetGamesOfUser(User user)
        {
            return GetGamesOfUser(user.User_id);
        }

        public List<User> GetUserOfGame(int game_id)
        {
            List<User> result = new List<User>();
            foreach (Link link in database.Links.FindAll(x => x.Game_id == game_id))
                result.Add(GetUser(link.User_id));
            return result;
        }

        public List<User> GetUserOfGame(Game game)
        {
            return GetUserOfGame(game.Game_id);
        }
        #endregion

        #region Add
        public bool AddUser(string vorname, string nachname, int user_id)
        {
            return AddUser(new User(vorname, nachname, user_id));
        }

        public bool AddUser(string vorname, string nachname)
        {
            return AddUser(new User(vorname, nachname, GetNextUser_id()));
        }

        public bool AddUser(User user)
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

        public void AddUser(List<User> user)
        {
            foreach (User cur in user)
                AddUser(cur);
        }

        public bool AddGame(string name, int id)
        {
            return AddGame(new Game(name, id));
        }

        public bool AddGame(string name)
        {
            return AddGame(new Game(name, GetNextGame_id()));
        }

        public bool AddGame(Game game)
        {
            if (game.Game_id == -1)
                game.Game_id = GetNextGame_id();

            if (!database.GameData.Exists(x => x.Game_id == game.Game_id || x.Name == game.Name))
            {
                database.GameData.Add(game);
                database.Save();
                return true;
            }
            else
                return false;

        }

        public void AddGame(List<Game> games)
        {
            foreach (Game cur in games)
                AddGame(cur);
        }

        public bool AddLink(int user_id, int game_id)
        {
            return AddLink(new Link(user_id, game_id));
        }

        public bool AddLink(Link link)
        {
            if (GetUser(link.User_id).Equals(null) || GetGame(link.Game_id).Equals(null))
                return false;

            if (!database.Links.Exists(x => x.User_id == link.User_id && x.Game_id == link.Game_id))
            {
                database.Links.Add(link);
                database.Save();
                return true;
            }
            else
                return false;
        }

        public void AddLink(List<Link> links)
        {
            foreach (Link cur in links)
                AddLink(cur);
        }
        #endregion

        #region Delete
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
        public bool DeleteGame(Game game)
        {
            if (database.GameData.Remove(game))
            {
                foreach (Link cur in database.Links.FindAll(x => x.Game_id == game.Game_id))
                    DeleteLink(cur);
                database.Save();

                return true;
            }
            else
                return false;
        }

        public void DeleteGame(List<Game> games)
        {
            foreach (Game cur in games)
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

        #region Update
        public bool UpdateUser(int user_id, string vorname, string nachname)
        {
            try
            {
                if (!String.IsNullOrEmpty(vorname))
                    database.UserData.Where(x => x.User_id == user_id).First().Firstname = vorname;
                if (!String.IsNullOrEmpty(nachname))
                    database.UserData.Where(x => x.User_id == user_id).First().Lastname = nachname;
                database.Save();

                return true;
            }
            catch (Exception)
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
                if (String.IsNullOrEmpty(name))
                    return false;
                database.GameData.Where(x => x.Game_id == game_id).First().Name = name;
                database.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateGame(Game game)
        {
            return UpdateGame(game.Game_id, game.Name);
        }
        #endregion

        #region Search
        public List<User> SearchUser(string firstname, string lastname)
        {
            if (String.IsNullOrEmpty(firstname) && String.IsNullOrEmpty(lastname))
                return GetUser();
            else if (String.IsNullOrEmpty(firstname))
                return database.UserData.FindAll(x => x.Lastname.Contains(lastname));
            else if (String.IsNullOrEmpty(lastname))
                return database.UserData.FindAll(x => x.Firstname.Contains(firstname));
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
        private static string FILEPATH = Settings.XmlDatabaseLocation;

        private List<User> userData;
        public List<User> UserData
        {
            get { return userData; }
            set { userData = value; }
        }

        private List<Game> gameData;
        public List<Game> GameData
        {
            get { return gameData; }
            set { gameData = value; }
        }

        private List<Link> links;
        public List<Link> Links
        {
            get { return links; }
            set { links = value; }
        }

        public XMLDatabase()
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Settings.XmlDatabaseLocation));

            userData = new List<User>();
            gameData = new List<Game>();
            links = new List<Link>();
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
            catch (Exception)
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