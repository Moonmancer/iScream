using iScream.GUI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace iScream.GUI.Classes
{
    class MenuVM : PropertyNotify
    {
        private ContentControl _container;

        public MenuVM(ContentControl container)
        {
            CloseApplicationCommand = new SimpleCommand(ExecuteCloseApplicationCommand);
            DisplayUserCommand = new SimpleCommand(ExecuteDisplayUserCommand);
            DisplayGameCommand = new SimpleCommand(ExecuteDisplayGameCommand);
            _container = container;
        }
        private void ExecuteDisplayUserCommand(object obj)
        {
            //View DisplayUser
            var control = new UserDisplay();
            control.DataContext = new DisplayUserVM(_container);
            _container.Content = control;
        }

        private void ExecuteDisplayGameCommand(object obj)
        {
            //View DisplayGroup
            var control = new GameDisplay();
            control.DataContext = new DisplayGameVM(_container);
            _container.Content = control;
        }
        
        private void ExecuteCloseApplicationCommand(object obj)
        {
            Environment.Exit(0);
        }
        
        public ICommand CloseApplicationCommand { get; private set; }
        public ICommand DisplayUserCommand { get; private set; }
        public ICommand DisplayGameCommand { get; private set; }

    }
}
}
