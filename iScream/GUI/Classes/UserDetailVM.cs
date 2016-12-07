using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream.GUI.Classes
{
    class UserDetailVM : PropertyNotify
    {

        private string _firstname;
        private string _lastname;
        private int _id;
        private User _user;

        public UserDetailVM(User user)
        {
            _user = user;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            ID = user.User_id;
        }

        public string Firstname
        {
            get { return _firstname; }
            set
            {
                if (_firstname != value)
                {
                    _firstname = value;
                    NotifyPropertyChanged("Firstname");
                }
            }
        }
        public string Lastname
        {
            get { return _lastname; }
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                    NotifyPropertyChanged("Lastname");
                }
            }
        }
        public int ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
    }
}
