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

        bool createUser(Nutzer user);
        bool createGame(Spiel game);

        #endregion

        #region Delete

        bool deleteUser(Nutzer user);
        bool deleteUser(int user_id);

        bool deleteGame(Spiel game);
        bool deleteGame(int spiel_id)
        #endregion

        #region Search

        #endregion

    }
}
