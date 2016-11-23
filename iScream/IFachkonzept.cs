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

        #region Details

        Container detailsUser(User user);
        Container detailsGame(Game game);

        #endregion

        #region Create

        bool createUser(User user);
        bool createGame(Game game);

        #endregion

        #region Delete

        bool deleteUser(User user);
        bool deleteUser(int user_id);

        bool deleteGame(Game game);
        bool deleteGame(int game_id);

        #endregion

        #region Search

        List<Game> searchGame(String name);
        List<User> searchUser(String firstname, String lastname);

        #endregion

    }
}
