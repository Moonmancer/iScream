using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    public class Game
    {
        #region Attributes
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private int game_id;
        public int Game_id
        {
            get
            {
                return game_id;
            }
            set
            {
                game_id = value;
            }
        }
        #endregion

        #region Constructors
        public Game()
        {

        }

        public Game(string name)
        {
            this.name = name;
            this.game_id = -1;
        }

<<<<<<< HEAD:iScream/Game.cs
        public Game(string name, int spiel_id)
=======
        public Game(string name, int game_id)
>>>>>>> refs/remotes/origin/Datenhaltung:iScream/Game.cs
        {
            this.name = name;
            this.game_id = game_id;
        }
        #endregion
    }
}
