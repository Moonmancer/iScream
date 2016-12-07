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
    class DisplayGameVM : PropertyNotify
    {
        private ContentControl _container;
        private int _id;
        private string _name;
        private Game _selectedItem;
        private List<Game> _displayedList;
        private IFachkonzept _fachkonzept;

        public DisplayGameVM(ContentControl container, IFachkonzept fachkonzept)
        {
            AddCommand = new SimpleCommand(ExecuteAddCommand);
            SearchCommand = new SimpleCommand(ExecuteSearchCommand);
            DeleteCommand = new SimpleCommand(ExecuteDeleteCommand);
            DetailCommand = new SimpleCommand(ExecuteDetailCommand);
            _container = container;
            _fachkonzept = fachkonzept;
            DisplayedList = _fachkonzept.getGames();
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
        public List<Game> DisplayedList
        {
            get { return _displayedList; }
            set
            {
                if (_displayedList != value)
                {
                    _displayedList = value;
                    NotifyPropertyChanged("DisplayedList");
                }
            }
        }

        private void ExecuteDetailCommand(object obj)
        {
            var gameDetailWin = new GameDetailVM(SelectedItem, _fachkonzept);
            var window = new GameDetails();
            window.DataContext = gameDetailWin;

            if (window.ShowDialog().Value)
            {
                Name = gameDetailWin.Name;
                Id = gameDetailWin.ID;
                _fachkonzept.updateGame(Id, Name);
            }
        }

        private void ExecuteDeleteCommand(object obj)
        {
            if (_fachkonzept.deleteGame(SelectedItem))
            {
                MessageBox.Show("Das ausgewählte Spiel wurde entfernt.");
            }
            else
            {
                MessageBox.Show("Das ausgewählte Spiel konnte nicht entfernt werden.");
            }
        }

        private void ExecuteSearchCommand(object obj)
        {
            var gameSearchWin = new GameSearchVM();
            var window = new GameSearch();
            window.DataContext = gameSearchWin;

            if (window.ShowDialog().Value)
            {
                var GameName = gameSearchWin.Name;
                DisplayedList = _fachkonzept.searchGame(GameName);
            }
        }

        private void ExecuteAddCommand(object obj)
        {
            var gameAddWin = new GameAddVM();
            var window = new GameAdd();
            window.DataContext = gameAddWin;

            if (window.ShowDialog().Value)
            {
                var NewGameName = gameAddWin.Name;
                var game = new Game(NewGameName);
                _fachkonzept.createGame(game);
            }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand DetailCommand { get; private set; }

    }
}
