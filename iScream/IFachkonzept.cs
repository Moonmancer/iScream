using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    interface IFachkonzept
    {
        #region Details
           
        // container detailsUser (Nutzer user);
        
        #endregion

        #region Create

        bool createUser(User user);
        bool createGame(Game game);

        #endregion

        #region Delete

        bool deleteUser(User user);
        bool deleteUser(int user_id);

        bool deleteGame(Game game);
        bool deleteGame(int spiel_id)
        #endregion

        #region Search

        #endregion

    }
}
