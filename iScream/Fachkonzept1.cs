using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    class Fachkonzept1 : IFachkonzept
    {

        IDatenhaltung datenhaltung;
        Fachkonzept1(IDatenhaltung datenhaltung)
        {
            this.datenhaltung = datenhaltung;
        }

        #region Details

        Container detailsUser(User user)
        {
            Container details;
            List<User> users = new List<User>{user};
            List<Game> games = new List<Game>();

            games = datenhaltung.GetGamesOfUser(user);

            details = new Container( users , games); 

            return details;
        }
        Container detailsGame(Game game)
        {
            Container details;
            List<User> users = new List<User>();
            List<Game> games = new List<Game>{game};

            users = datenhaltung.GetUserOfGame(game);

            details = new Container(users, games);

            return details;
        }

        #endregion

        #region Create

        bool createUser(User user)
        {
            return datenhaltung.AddUser(user);
        }
        bool createGame(Game game)
        {
            return datenhaltung.AddGame(game);
        }

        #endregion

        #region Delete

        bool deleteUser(User user)
        {
            return datenhaltung.DeleteUser(user);
        }

        bool deleteUser(int user_id)
        {
            return datenhaltung.DeleteUser(user_id);
        }

        bool deleteGame(Game game)
        {
            return datenhaltung.DeleteGame(game);
        }
        bool deleteGame(int game_id)
        {
            return datenhaltung.DeleteGame(game_id);
        }

        #endregion

        #region Search

        List<Game> searchGame(String name)
        {
            return datenhaltung.SearchGame(name);
        }
        List<User> searchUser(String firstname , String lastname)
        {
            return datenhaltung.SearchUser(firstname, lastname);
        }

        #endregion
    }
}
