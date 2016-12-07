using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private IFachkonzept _fachkonzept;
        private List<User> _displayedList;

        public DisplayUserVM(ContentControl container, IFachkonzept fachkonzept)
        {
            AddCommand = new SimpleCommand(ExecuteAddCommand);
            SearchCommand = new SimpleCommand(ExecuteSearchCommand);
            DeleteCommand = new SimpleCommand(ExecuteDeleteCommand);
            DetailCommand = new SimpleCommand(ExecuteDetailCommand);
            _container = container;
            _fachkonzept = fachkonzept;
            //DisplayedList = _fachkonzept. //TODO: Alle User ausgeben!
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
        public List<User> DisplayedList
        {
            get { return _displayedList; }
            set
            {
                if(_displayedList != value)
                {
                    _displayedList = value;
                    NotifyPropertyChanged("DisplayedList");
                }
            }
        }

        private void ExecuteDetailCommand(object obj)
        {
            var userDetailWin = new UserDetailVM(SelectedItem);
            var window = new UserDetails();
            window.DataContext = userDetailWin;

            if (window.ShowDialog().Value)
            {

                //TODO: Update User
                Firstname = userDetailWin.Firstname;
                Lastame = userDetailWin.Lastname;
                Id = userDetailWin.ID;
                //DisplayedList = _fachkonzept.
            }
        }

        private void ExecuteDeleteCommand(object obj)
        {
           if(_fachkonzept.deleteUser(SelectedItem))
            {
                MessageBox.Show("Der ausgewählte User wurde entfernt.");
            }else
            {
                MessageBox.Show("Der ausgewählte User konnte nicht entfernt werden.");
            }
        }

        private void ExecuteSearchCommand(object obj)
        {
            var userSearchWin = new UserSearchVM();
            var window = new UserSearch();
            window.DataContext = userSearchWin;

            if (window.ShowDialog().Value)
            {
                var UserFirstName = userSearchWin.Firstname;
                var UserLastName = userSearchWin.Lastname;
                DisplayedList = _fachkonzept.searchUser(UserFirstName, UserLastName);                
            }
        }

        private void ExecuteAddCommand(object obj)
        {
            var userAddWin = new UserAddVM();
            var window = new UserAdd();
            window.DataContext = userAddWin;

            if (window.ShowDialog().Value)
            {
                var NewUserFirstName = userAddWin.Firstname;
                var NewUserLastName = userAddWin.Lastname;
                var user = new User(NewUserFirstName, NewUserLastName);
                _fachkonzept.createUser(user);             
            }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand DetailCommand { get; private set; }

    }
}
