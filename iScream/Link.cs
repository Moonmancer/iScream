using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    public class Link
    {
        private int nutzer_id;
        public int Nutzer_id
        {
            get { return nutzer_id; }
            set { nutzer_id = value; }
        }

        private int spiel_id;
        public int Spiel_id
        {
            get { return spiel_id; }
            set { spiel_id = value; }
        }

        public Link()
        {
            nutzer_id = new int();
            spiel_id = new int();
        }

        public Link(int nutzer_id, int spiel_id)
        {
            this.nutzer_id = nutzer_id;
            this.spiel_id = spiel_id;
        }
    }
}
