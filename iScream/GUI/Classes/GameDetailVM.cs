using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iScream.GUI.Classes
{
    class GameDetailVM : PropertyNotify
    {
        private string _name;
        private int _id;
        private Game _game;
        private IFachkonzept _fachkonzept;

        public GameDetailVM(Game game, IFachkonzept fachkonzept)
        {
            _fachkonzept = fachkonzept;
            UserAddCommand = new SimpleCommand(ExecuteUserAddCommand);
            _game = game;
            Name = game.Name;
            ID = game.Game_id;
            //TODO:Add user list
        }

        private void ExecuteUserAddCommand(object obj)
        {
            throw new NotImplementedException();
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

        public ICommand UserAddCommand { get; private set; }
    }
}
