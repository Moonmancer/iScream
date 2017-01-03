using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace iScream.GUI.Classes
{
    class GameDetailVM : PropertyNotify
    {
        private string _name;
        private int _id;
        private Game _game;
        private IFachkonzept _fachkonzept;
        private List<User> _userList;
        private User _selectedItem;

        public GameDetailVM(Game game, IFachkonzept fachkonzept)
        {
            _fachkonzept = fachkonzept;
            UserAddCommand = new SimpleCommand(ExecuteUserAddCommand);
            UserDeleteCommand = new SimpleCommand(ExecuteUserDeleteCommand);
            _game = game;
            Name = game.Name;
            ID = game.Game_id;
            UserListFiles = _fachkonzept.detailsGame(game).Users;
        }

        private void ExecuteUserDeleteCommand(object obj)
        {
            try
            {
                _fachkonzept.deleteLink(SelectedItem.User_id, ID);
            }
            catch (Exception)
            {
                MessageBox.Show("Bitte wählen sie ein User zum entfernen aus");
            }
        }

        private void ExecuteUserAddCommand(object obj)
        {
            var gameAddUserWin = new GameAddUserVM();
            var window = new GameAddUser();
            window.DataContext = gameAddUserWin;
            window.MaxHeight = 120;
            window.MaxWidth = 300;

            if (window.ShowDialog().Value)
            {
                var NewGameID = gameAddUserWin.ID;
                var link = new Link(NewGameID, ID);
                _fachkonzept.createLink(link);
            }
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
        public List<User> UserListFiles
        {
            get { return _userList; }
            set
            {
                if (_userList != value)
                {
                    _userList = value;
                    NotifyPropertyChanged("UserListFiles");
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


        public ICommand UserAddCommand { get; private set; }
        public ICommand UserDeleteCommand { get; private set; }
    }
}
