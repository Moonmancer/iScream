using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace iScream.GUI.Classes
{
    public class GUIStartup
    {

        public GUIStartup(IFachkonzept fachkonzept)
        {
            GUIfachkonzept = fachkonzept;
        }

        public static IFachkonzept GUIfachkonzept { get; set; }

        [STAThread]
        public void Run()
        {
            //GUIMain GUI = new GUIMain();
            //GUI.Activate();
            Application app = new Application();
            //Threading.Thread.CurrentThread.ApartmentState = Threading.ApartmentState.STA;
            GUI.GUIMain UI = new GUI.GUIMain(GUIfachkonzept);
            //app.Run(UI);
            //UI.MaxHeight = 500;
            //UI.MaxWidth = 500;
            UI.ShowDialog();
        }

    }
}
