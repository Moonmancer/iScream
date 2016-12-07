using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    interface IFachkonzept
    {
        //doesnt work:
        //IFachkonzept(IDatenhaltung datenhaltung);

        #region List

        List<User> getUsers();
        List<Game> getGames();
        List<Link> getLinks();

        #endregion

        #region Update

        bool updateUser(int user_id, string firstname, string lastname);
        bool updateUser(User user);

        bool updateGame(int game_id, string name);
        bool updateGame(Game game);

        #endregion

        #region Details

        Container detailsUser(User user);
        Container detailsGame(Game game);

        #endregion

        #region Create

        bool createUser(User user);
        bool createGame(Game game);
        bool createLink(Link link);

        #endregion

        #region Delete

        bool deleteUser(User user);
        bool deleteUser(int user_id);

        bool deleteGame(Game game);
        bool deleteGame(int game_id);

        bool deleteLink(Link link);
        bool deleteLink(int user_id, int game_id);
        #endregion

        #region Search

        List<Game> searchGame(String name);
        List<User> searchUser(String firstname, String lastname);

        #endregion

        void changeSortOrder();
    }
}
