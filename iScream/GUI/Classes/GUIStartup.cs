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
        public void RunGUI()
        {
            //GUIMain GUI = new GUIMain();
            //GUI.Activate();
            Application app = new Application();
            //Threading.Thread.CurrentThread.ApartmentState = Threading.ApartmentState.STA;
            GUI.GUIMain UI = new GUI.GUIMain();
            app.Run(UI);
        }

    }
}
