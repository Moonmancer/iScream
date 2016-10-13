using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    public class Spiel
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

        private int spiel_id;
        public int Spiel_id
        {
            get
            {
                return spiel_id;
            }
            set
            {
                spiel_id = value;
            }
        }
        #endregion

        #region Constructors
        public Spiel()
        {

        }

        public Spiel(string name)
        {
            this.name = name;
        }

        public Spiel(string name, int spiel_id)
        {
            this.name = name;
            this.spiel_id = spiel_id;
        }
        #endregion
    }
}
