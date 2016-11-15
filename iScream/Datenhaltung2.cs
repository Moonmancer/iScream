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
            if (database.NutzerDaten.Count > 0)
                return database.NutzerDaten.Last().Nutzer_id + 1;
            else
                return 1;
        }

        public int HoleNächsteSpiel_id()
        {
            if (database.SpielDaten.Count > 0)
                return database.SpielDaten.Last().Spiel_id + 1;
            else
                return 1;
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

        public Verknüpfung HoleVerknüpfung(int nutzer_id, int spiel_id)
        {
            throw new NotImplementedException();
        }

        public Verknüpfung HoleVerknüpfung(Verknüpfung verknüpfung)
        {
            throw new NotImplementedException();
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
        public bool FügeNutzerHinzu(string vorname, string nachname, int nutzer_id)
        {
            return FügeNutzerHinzu(new Nutzer(vorname, nachname, nutzer_id));
        }

        public bool FügeNutzerHinzu(string vorname, string nachname)
        {
            return FügeNutzerHinzu(new Nutzer(vorname, nachname, HoleNächsteNutzer_id()));
        }

        public bool FügeNutzerHinzu(Nutzer nutzer)
        {
            if (nutzer.Nutzer_id == -1)
                nutzer.Nutzer_id = HoleNächsteNutzer_id();

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

        public bool FügeSpielHinzu(string name)
        {
            return FügeSpielHinzu(new Spiel(name, HoleNächsteSpiel_id()));
        }

        public bool FügeSpielHinzu(Spiel spiel)
        {
            if (spiel.Spiel_id == -1)
                spiel.Spiel_id = HoleNächsteSpiel_id();

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
                FügeVerknüpfungHinzu(cur);
        }
        #endregion

        #region Löschen
        public bool LöscheNutzer(int nutzer_id)
        {
            return LöscheNutzer(database.NutzerDaten.Find(x => x.Nutzer_id == nutzer_id));
        }

        public bool LöscheNutzer(Nutzer nutzer)
        {
            if (database.NutzerDaten.Remove(nutzer))
            {
                foreach (Verknüpfung cur in database.Verknüpfungen.FindAll(x => x.Nutzer_id == nutzer.Nutzer_id))
                    LöscheVerknüpfung(cur);
                database.Save();

                return true;
            }
            else
                return false;
        }
        public void LöscheNutzer(List<Nutzer> nutzer)
        {
            foreach (Nutzer cur in nutzer)
                LöscheNutzer(cur);
        }

        public bool LöscheSpiel(int spiel_id)
        {
            return LöscheSpiel(database.SpielDaten.Find(x => x.Spiel_id == spiel_id));
        }
        public bool LöscheSpiel(Spiel spiel)
        {
            if (database.SpielDaten.Remove(spiel))
            {
                foreach (Verknüpfung cur in database.Verknüpfungen.FindAll(x => x.Spiel_id == spiel.Spiel_id))
                    LöscheVerknüpfung(cur);
                database.Save();

                return true;
            }
            else
                return false;
        }
        public void LöscheSpiel(List<Spiel> spiele)
        {
            foreach (Spiel cur in spiele)
                LöscheSpiel(cur);
        }

        public bool LöscheVerknüpfung(int nutzer_id, int spiel_id)
        {
            return LöscheVerknüpfung(database.Verknüpfungen.Find(x => x.Nutzer_id == nutzer_id && x.Spiel_id == spiel_id));
        }
        public bool LöscheVerknüpfung(Verknüpfung verknüpfung)
        {
            if (database.Verknüpfungen.Remove(verknüpfung))
            {
                database.Save();

                return true;
            }
            else
                return false;

        }
        #endregion

        #region Ändern
        public bool ÄndereNutzer(int nutzer_id, string vorname, string nachname)
        {
            try
            {
                database.NutzerDaten.Where(x => x.Nutzer_id == nutzer_id).First().Vorname = vorname;
                database.NutzerDaten.Where(x => x.Nutzer_id == nutzer_id).First().Nachname = nachname;
                database.Save();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ÄndereNutzer(Nutzer nutzer)
        {
            return ÄndereNutzer(nutzer.Nutzer_id, nutzer.Vorname, nutzer.Nachname);
        }

        public bool ÄndereSpiel(int spiel_id, string name)
        {
            try
            {
                database.SpielDaten.Where(x => x.Spiel_id == spiel_id).First().Name = name;
                database.Save();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ÄndereSpiel(Spiel spiel)
        {
            return ÄndereSpiel(spiel.Spiel_id, spiel.Name);
        }
        #endregion

        #region Suchen
        public List<Nutzer> SucheNutzer(string vorname, string nachname)
        {
            if (String.IsNullOrEmpty(vorname) && String.IsNullOrEmpty(nachname))
                return HoleNutzer();
            else 
                return database.NutzerDaten.FindAll(x => x.Vorname.Contains(vorname) && x.Nachname.Contains(nachname));
        }

        public List<Spiel> SucheSpiel(string name)
        {
            if (String.IsNullOrEmpty(name))
                return HoleSpiel();
            else
                return database.SpielDaten.FindAll(x => x.Name.Contains(name));
        }
        #endregion
    }

    public class XMLDatabase
    {
        private static string FILEPATH = Settings.CurrentSettings.XmlDatabaseLocation;

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
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Settings.CurrentSettings.XmlDatabaseLocation));

            nutzerDaten = new List<Nutzer>();
            spielDaten = new List<Spiel>();
            verknüpfungen = new List<Verknüpfung>();
        }

        public bool Load()
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(FILEPATH, System.IO.FileMode.Open))
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
            using (System.IO.FileStream fs = new System.IO.FileStream(FILEPATH, System.IO.FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(XMLDatabase));
                xs.Serialize(fs, this);
            }
        }
    }
}
