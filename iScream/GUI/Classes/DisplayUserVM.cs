using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace iScream.GUI.Classes
{
    class DisplayUserVM : PropertyNotify
    {
        private ContentControl _container;
        private string _firstname;
        private int _id;
        private string _lastname;
        private User _selectedItem;

        public DisplayUserVM(ContentControl container)
        {
            AddCommand = new SimpleCommand(ExecuteAddCommand);
            SearchCommand = new SimpleCommand(ExecuteSearchCommand);
            DeleteCommand = new SimpleCommand(ExecuteDeleteCommand);
            DetailCommand = new SimpleCommand(ExecuteDetailCommand);
            _container = container;
            var UserList = 
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
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }
        public string Lastame
        {
            get { return _lastname; }
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                    NotifyPropertyChanged("Lastame");
                }
            }
        }
        public User SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    NotifyPropertyChanged("SelectedItem");
                }
            }
        }

        private void ExecuteDetailCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteDeleteCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteSearchCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteAddCommand(object obj)
        {
            throw new NotImplementedException();
        }

        public ICommand AddCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand DetailCommand { get; private set; }

    }
}
