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


        static void Main(string[] args)
        {
            //Main should always start with Settings.Load()
            Settings.Load();

            DatenhaltungExtras.ImportCSV(".\\iScreamExport", new Datenhaltung2());

            Datenhaltung2 dh2 = new Datenhaltung2();
            TUI.TUI UI = new TUI.TUI(new Fachkonzept2(dh2));
            //GUI.Classes.MenuVM UI = new GUI.Classes.MenuVM(new Fachkonzept1(dh2));

            UI.MainMenu();

            //Main should always end with Settings.Save()
            DatenhaltungExtras.ExportCSV(dh2, ".\\iScreamExport");
            Settings.Save();
        }
    }
}