using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace iScream
{
    class Datenhaltung2 : IDatenhaltung
    {
        private XMLDatabase database;

        public Datenhaltung2()
        {
            database = new XMLDatabase();
            database.Load();
        }

        #region Holen
        public int HoleNächsteNutzer_id()
        {
            return database.NutzerDaten.Last().Nutzer_id + 1;
        }

        public int HoleNächsteSpiel_id()
        {
            return database.SpielDaten.Last().Spiel_id + 1;
        }

        public Nutzer HoleNutzer(int nutzer_id)
        {
            return database.NutzerDaten.Find(x => x.Nutzer_id == nutzer_id);
        }

        public List<Nutzer> HoleNutzer()
        {
            return database.NutzerDaten;
        }

        public Spiel HoleSpiel(int spiel_id)
        {
            return database.SpielDaten.Find(x => x.Spiel_id == spiel_id);
        }

        public List<Spiel> HoleSpiel()
        {
            return database.SpielDaten;
        }

        public List<Verknüpfung> HoleVerknüpfung()
        {
            return database.Verknüpfungen;
        }

        public List<Spiel> HoleSpieleVonNutzer(int nutzer_id)
        {
            List<Spiel> result = new List<Spiel>();
            List<Verknüpfung> tmp = database.Verknüpfungen.FindAll(x => x.Nutzer_id == nutzer_id);
            foreach (Verknüpfung verknüpfung in tmp)
                result.Add(HoleSpiel(verknüpfung.Spiel_id));
            return result;
        }
        #region HoleSpieleVonNutzer-Overloads
        public List<Spiel> HoleSpieleVonNutzer(Nutzer nutzer)
        {
            return HoleSpieleVonNutzer(nutzer.Nutzer_id);
        }
        #endregion

        public List<Nutzer> HoleNutzerVonSpiel(int spiel_id)
        {
            List<Nutzer> result = new List<Nutzer>();
            foreach (Verknüpfung verknüpfung in database.Verknüpfungen.FindAll(x => x.Spiel_id == spiel_id))
                result.Add(HoleNutzer(verknüpfung.Nutzer_id));
            return result;
        }
        #region HoleNutzerVonSpiel-Overloads
        public List<Nutzer> HoleNutzerVonSpiel(Spiel spiel)
        {
            return HoleNutzerVonSpiel(spiel.Spiel_id);
        }
        #endregion
        #endregion

        #region Hinzufügen
        public bool FügeNutzerHinzu(string vorname, string nachname, int id)
        {
            return FügeNutzerHinzu(new Nutzer(vorname, nachname, id));
        }

        public bool FügeNutzerHinzu(Nutzer nutzer)
        {
            if (!database.NutzerDaten.Exists(x => x.Nutzer_id == nutzer.Nutzer_id))
            {
                database.NutzerDaten.Add(nutzer);
                database.Save();
                return true;
            }
            else
                return false;
        }

        public void FügeNutzerHinzu(List<Nutzer> nutzer)
        {
            foreach (Nutzer cur in nutzer)
                FügeNutzerHinzu(cur);
        }

        public bool FügeSpielHinzu(string name, int id)
        {
            return FügeSpielHinzu(new Spiel(name, id));
        }

        public bool FügeSpielHinzu(Spiel spiel)
        {
            if (!database.SpielDaten.Exists(x => x.Spiel_id == spiel.Spiel_id))
            {
                database.SpielDaten.Add(spiel);
                database.Save();
                return true;
            }
            else
                return false;

        }

        public void FügeSpielHinzu(List<Spiel> spiele)
        {
            foreach (Spiel cur in spiele)
                FügeSpielHinzu(cur);
        }

        public bool FügeVerknüpfungHinzu(int nutzer_id, int spiel_id)
        {
            return FügeVerknüpfungHinzu(new Verknüpfung(nutzer_id, spiel_id));
        }

        public bool FügeVerknüpfungHinzu(Verknüpfung verknüpfung)
        {
            if (!database.Verknüpfungen.Exists(x => x.Nutzer_id == verknüpfung.Nutzer_id && x.Spiel_id == verknüpfung.Spiel_id))
            {
                database.Verknüpfungen.Add(verknüpfung);
                database.Save();
                return true;
            }
            else
                return false;
        }

        public void FügeVerknüpfungHinzu(List<Verknüpfung> verknüpfungen)
        {
            foreach (Verknüpfung cur in verknüpfungen)
                FügeVerknüpfungHinzu(cur.Nutzer_id, cur.Spiel_id);
        }
        #endregion

        #region Löschen
        #endregion

        #region Ändern
        #endregion
    }

    public class XMLDatabase
    {
        private static string FILEPATH = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "iScream");
        private static string FILENAME = "XMLDatabase.xml";

        private List<Nutzer> nutzerDaten;
        public List<Nutzer> NutzerDaten
        {
            get { return nutzerDaten; }
            set { nutzerDaten = value; }
        }

        private List<Spiel> spielDaten;
        public List<Spiel> SpielDaten
        {
            get { return spielDaten; }
            set { spielDaten = value; }
        }

        private List<Verknüpfung> verknüpfungen;
        public List<Verknüpfung> Verknüpfungen
        {
            get { return verknüpfungen; }
            set { verknüpfungen = value; }
        }

        public XMLDatabase()
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "iScream"));

            nutzerDaten = new List<Nutzer>();
            spielDaten = new List<Spiel>();
            verknüpfungen = new List<Verknüpfung>();
        }

        public bool Load()
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(System.IO.Path.Combine(FILEPATH, FILENAME), System.IO.FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(XMLDatabase));
                    XMLDatabase tmp = (XMLDatabase)xs.Deserialize(fs);

                    this.nutzerDaten = tmp.nutzerDaten;
                    this.spielDaten = tmp.spielDaten;
                    this.verknüpfungen = tmp.verknüpfungen;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Save()
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(System.IO.Path.Combine(FILEPATH, FILENAME), System.IO.FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(XMLDatabase));
                xs.Serialize(fs, this);
            }
        }
    }
}
