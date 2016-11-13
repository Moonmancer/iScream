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

            //Tests.DatenhaltungTests.Run(false);


            List<Nutzer> nutzers = new List<Nutzer>
            {
                new Nutzer("Mike", "Rohsoft"),
                new Nutzer("Ann", "Droid"),
                new Nutzer("Sam", "Sung")
            };

            List<Spiel> spiele = new List<Spiel>
            {
                new Spiel("Tetris"),
                new Spiel("Minecraft")
            };
            IDatenhaltung dh1 = new Datenhaltung1();
            IDatenhaltung dh2 = new Datenhaltung2();

            dh1.FügeNutzerHinzu(nutzers);
            dh1.FügeSpielHinzu(spiele);

            Console.ReadLine();
        }
    }
}
