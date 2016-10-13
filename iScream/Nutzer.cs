using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    public class Nutzer
    {
        #region Attributes
        private string vorname;
        public string Vorname
        {
            get
            {
                return vorname;
            }
            set
            {
                vorname = value;
            }
        }

        private string nachname;
        public string Nachname
        {
            get
            {
                return nachname;
            }
            set
            {
                nachname = value;
            }
        }

        public string Name
        {
            get
            {
                return Vorname + " " + Nachname;
            }
        }

        private int nutzer_id;
        public int Nutzer_id
        {
            get
            {
                return nutzer_id;
            }
            set
            {
                nutzer_id = value;
            }
        }
        #endregion

        #region Constructors
        public Nutzer()
        {

        }

        public Nutzer(string vorname, string nachname)
        {
            this.vorname = vorname;
            this.nachname = nachname;
        }

        public Nutzer(string vorname, string nachname, int nutzer_id)
        {
            this.vorname = vorname;
            this.nachname = nachname;
            this.nutzer_id = nutzer_id;
        }
        #endregion
    }
}
