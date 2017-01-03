using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace iScream
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Main should always start with Settings.Load()
            Settings.Load();


            Datenhaltung2 dh2 = new Datenhaltung2();
            //TUI.TUI UI = new TUI.TUI(new Fachkonzept2(dh2));
            GUI.Classes.GUIStartup UI = new GUI.Classes.GUIStartup(new Fachkonzept1(dh2));


            UI.Run();

            //Main should always end with Settings.Save()
            Settings.Save();
        }
    }
}