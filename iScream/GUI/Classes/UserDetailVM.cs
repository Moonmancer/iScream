using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iScream.GUI.Classes
{
    class UserDetailVM : PropertyNotify
    {

        private string _firstname;
        private string _lastname;
        private int _id;
        private User _user;
        private List<Game> _gameList;
        private IFachkonzept _fachkonzept;
        private Game _selectedItem;

        public UserDetailVM(User user, IFachkonzept fachkonzept)
        {
            _fachkonzept = fachkonzept;
            GameAddCommand = new SimpleCommand(ExecuteGameAddCommand);
            GameDeleteCommand = new SimpleCommand(ExecuteGameDeleteCommand);
            _user = user;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            ID = user.User_id;
            GameListFiles = _fachkonzept.detailsUser(user).Games;
        }

        private void ExecuteGameDeleteCommand(object obj)
        {
            _fachkonzept.deleteLink(ID, SelectedItem.Game_id);
        }

        private void ExecuteGameAddCommand(object obj)
        {
            var userAddGamesWin = new UserAddGameVM();
            var window = new UserAddGames();
            window.DataContext = userAddGamesWin;

            if (window.ShowDialog().Value)
            {
                var NewGameID = userAddGamesWin.ID;
                var link = new Link(ID, NewGameID);
                _fachkonzept.createLink(link);
            }
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
        public List<Game> GameListFiles
        {
            get { return _gameList; }
            set
            {
                if (_gameList != value)
                {
                    _gameList = value;
                    NotifyPropertyChanged("GameListFiles");
                }
            }
        }
        public Game SelectedItem
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


        public ICommand GameAddCommand { get; private set; }
        public ICommand GameDeleteCommand { get; private set; }
    }
}
