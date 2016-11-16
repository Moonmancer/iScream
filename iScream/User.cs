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

        public User(string firstname, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
        }

        public User(string firstname, string lastname, int user_id)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.user_id = user_id;
        }
        #endregion
    }
}
