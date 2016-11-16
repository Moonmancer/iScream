using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    public class User
    {
        #region Attributes
        private string firstname;
        public string Firstname
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
            }
        }

        private string lastname;
        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
            }
        }

        public string Name
        {
            get
            {
                return Firstname + " " + Lastname;
            }
        }

        private int user_id;
        public int User_id
        {
            get
            {
                return user_id;
            }
            set
            {
                user_id = value;
            }
        }
        #endregion

        #region Constructors
        public User()
        {

        }

        public User(string vorname, string lastname)
        {
            this.firstname = vorname;
            this.lastname = lastname;
            this.user_id = -1;
        }

        public User(string vorname, string lastname, int user_id)
        {
            this.firstname = vorname;
            this.lastname = lastname;
            this.user_id = user_id;
        }
        #endregion
    }
}
