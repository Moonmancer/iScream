﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream.GUI.Classes
{
    class GameAddVM : PropertyNotify
    {
        private string _name;

        public GameAddVM()
        {

        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
    }
}
