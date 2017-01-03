using iScream.GUI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iScream.GUI
{
    /// <summary>
    /// Interaction logic for GUIMain.xaml
    /// </summary>
    public partial class GUIMain : Window
    {
        public GUIMain(IFachkonzept fachkonzept)
        {
            InitializeComponent();
            Menu.DataContext = new MenuVM(Menu.Container, fachkonzept);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }


        public MenuControl MenuControl { get { return Menu; } }

        //static IFachkonzept GUIfachkonzept { get; set; }

    }
}
