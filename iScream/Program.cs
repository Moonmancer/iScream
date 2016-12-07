﻿using System;
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
            Fachkonzept1 fh1 = new Fachkonzept1(dh2);
            //DHTUI dhtui = new DHTUI(dh2);
            TUI.TUI tuitest = new TUI.TUI(fh1, dh2);

            tuitest.Run();
            //dhtui.Run();

            //Main should always end with Settings.Save()
            DatenhaltungExtras.ExportCSV(dh2, ".\\iScreamExport");
            Settings.Save();
        }
    }
}