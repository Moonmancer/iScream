using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream.GUI.Classes
{
    class UserAddVM : PropertyNotify
    {
        private string _firstname;
        private string _lastname;

        public UserAddVM()
        {

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
    }
}
