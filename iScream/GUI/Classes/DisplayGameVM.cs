using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace iScream.GUI.Classes
{
    class DisplayGameVM : PropertyNotify
    {
        private ContentControl _container;

        public DisplayGameVM(ContentControl container)
        {
            AddCommand = new SimpleCommand(ExecuteAddCommand);
            SearchCommand = new SimpleCommand(ExecuteSearchCommand);
            DeleteCommand = new SimpleCommand(ExecuteDeleteCommand);
            DetailCommand = new SimpleCommand(ExecuteDetailCommand);
            _container = container;
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
