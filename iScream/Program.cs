using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    class Program
    {
        static void Main(string[] args)
        {

            //Datenhaltung2 dh = new Datenhaltung2();

            /*
            bool init = SQL.Init();
            if (init)
                Console.WriteLine("Success!");
            else
                Console.WriteLine("Failed!");
            Console.ReadLine();
            */
            /*
            dh.FügeNutzerHinzu(new Nutzer("Hans", "Peter", 1));
            dh.FügeNutzerHinzu(new Nutzer("Ute", "Petersen", 2));
            dh.FügeNutzerHinzu(new Nutzer("Klara", "Himmel", 3));

            dh.FügeSpielHinzu(new Spiel("Call Of Booty", 1));
            dh.FügeSpielHinzu(new Spiel("Assasins Sheesh", 2));

            dh.FügeVerknüpfungHinzu(1, 1);
            dh.FügeVerknüpfungHinzu(2, 1);
            dh.FügeVerknüpfungHinzu(3, 1);

            dh.FügeVerknüpfungHinzu(1, 2);
            dh.FügeVerknüpfungHinzu(3, 2);
            */

            DatenhaltungTests.Run();
        }
    }
}
