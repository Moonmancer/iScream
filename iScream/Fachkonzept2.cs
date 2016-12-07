using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    class Fachkonzept2:IFachkonzept
    {
        //sortiert
        IDatenhaltung datenhaltung;
        Sorter SortHandler = new Sorter();
        private bool sortDescending
        {
            get
            {
                return sortDescending;
            }
            set
            {
                this.sortDescending = value;
                if (value == true)
                {
                    SortHandler.SetSortAlgorithm( new SortDescending());
                }
                else
                {
                    SortHandler.SetSortAlgorithm(new SortAscending());
                }
            }
        }

        public Fachkonzept2(IDatenhaltung datenhaltung)
        {
            this.datenhaltung = datenhaltung;
        }
        public Fachkonzept2(IDatenhaltung datenhaltung, bool sortDescending)
        {
            this.datenhaltung   = datenhaltung;
            this.sortDescending = sortDescending;
        }



        #region Details

        public Container detailsUser(User user)
        {
            Container details;
            List<User> users = new List<User> { user };
            List<Game> games = new List<Game>();

            games = datenhaltung.GetGamesOfUser(user);

            details = new Container(users, games);

            

            return details;
        }
        public Container detailsGame(Game game)
        {
            Container details;
            List<User> users = new List<User>();
            List<Game> games = new List<Game> { game };

            users = datenhaltung.GetUserOfGame(game);

            details = new Container(users, games);

            return details;
        }

        #endregion

        #region Create

        public bool createUser(User user)
        {
            return datenhaltung.AddUser(user);
        }
        public bool createGame(Game game)
        {
            return datenhaltung.AddGame(game);
        }
        public bool createLink(Link link)
        {
            return datenhaltung.AddLink(link.User_id, link.Game_id);
        }
        #endregion

        #region Delete

        public bool deleteUser(User user)
        {
            return datenhaltung.DeleteUser(user);
        }

        public bool deleteUser(int user_id)
        {
            return datenhaltung.DeleteUser(user_id);
        }

        public bool deleteGame(Game game)
        {
            return datenhaltung.DeleteGame(game);
        }
        public bool deleteGame(int game_id)
        {
            return datenhaltung.DeleteGame(game_id);
        }

        public bool deleteLink(Link link)
        {
            return datenhaltung.DeleteLink(link);
        }

        public bool deleteLink(int user_id, int game_id)
        {
            return datenhaltung.DeleteLink(user_id, game_id);
        }
        #endregion

        #region Search

        public List<Game> searchGame(String name)
        {
            return datenhaltung.SearchGame(name);
        }
        public List<User> searchUser(String firstname, String lastname)
        {
            return datenhaltung.SearchUser(firstname, lastname);
        }

        #endregion

        #region List

        public List<User> getUsers()
        {
            return datenhaltung.GetUser();
        }

        public List<Game> getGames()
        {
            return datenhaltung.GetGame();
        }

        public List<Link> getLinks()
        {
            return datenhaltung.GetLink();
        }

        #endregion

        #region Update

        public bool updateUser(int user_id, string firstname, string lastname)
        {
            return datenhaltung.UpdateUser(user_id, firstname, lastname);
        }

        public bool updateUser(User user)
        {
            return datenhaltung.UpdateUser(user);
        }

        public bool updateGame(int game_id, string name)
        {
            return datenhaltung.UpdateGame(game_id, name);
        }

        public bool updateGame(Game game)
        {
            return datenhaltung.UpdateGame(game);
        }
        #endregion

        void changeSortOrder()
        {
            sortDescending = !sortDescending;
        }
    }
}
