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

        public User HoleNutzer(int nutzer_id)
        {
            return database.NutzerDaten.Find(x => x.Nutzer_id == nutzer_id);
        }

        public List<User> HoleNutzer()
        {
            return database.NutzerDaten;
        }

        public Game HoleSpiel(int spiel_id)
        {
            return database.SpielDaten.Find(x => x.Spiel_id == spiel_id);
        }

        public List<Game> HoleSpiel()
        {
            return database.SpielDaten;
        }

        public List<Link> HoleVerknüpfung()
        {
            return database.Verknüpfungen;
        }

        public List<Game> HoleSpieleVonNutzer(int nutzer_id)
        {
            List<Game> result = new List<Game>();
            List<Link> tmp = database.Verknüpfungen.FindAll(x => x.Nutzer_id == nutzer_id);
            foreach (Link verknüpfung in tmp)
                result.Add(HoleSpiel(verknüpfung.Spiel_id));
            return result;
        }
        #region HoleSpieleVonNutzer-Overloads
        public List<Game> HoleSpieleVonNutzer(User nutzer)
        {
            return HoleSpieleVonNutzer(nutzer.Nutzer_id);
        }
        #endregion

        public List<User> HoleNutzerVonSpiel(int spiel_id)
        {
            List<User> result = new List<User>();
            foreach (Link verknüpfung in database.Verknüpfungen.FindAll(x => x.Spiel_id == spiel_id))
                result.Add(HoleNutzer(verknüpfung.Nutzer_id));
            return result;
        }
        #region HoleNutzerVonSpiel-Overloads
        public List<User> HoleNutzerVonSpiel(Game spiel)
        {
            return HoleNutzerVonSpiel(spiel.Spiel_id);
        }
        #endregion
        #endregion

        #region Hinzufügen
        public bool FügeNutzerHinzu(string vorname, string nachname, int id)
        {
            return FügeNutzerHinzu(new User(vorname, nachname, id));
        }

        public bool FügeNutzerHinzu(User nutzer)
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

        public void FügeNutzerHinzu(List<User> nutzer)
        {
            foreach (User cur in nutzer)
                FügeNutzerHinzu(cur);
        }

        public bool FügeSpielHinzu(string name, int id)
        {
            return FügeSpielHinzu(new Game(name, id));
        }

        public bool FügeSpielHinzu(Game spiel)
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

        public void FügeSpielHinzu(List<Game> spiele)
        {
            foreach (Game cur in spiele)
                FügeSpielHinzu(cur);
        }

        public bool FügeVerknüpfungHinzu(int nutzer_id, int spiel_id)
        {
            return FügeVerknüpfungHinzu(new Link(nutzer_id, spiel_id));
        }

        public bool FügeVerknüpfungHinzu(Link verknüpfung)
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

        public void FügeVerknüpfungHinzu(List<Link> verknüpfungen)
        {
            foreach (Link cur in verknüpfungen)
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
        private static string FILEPATH = Settings.CurrentSettings.XmlDatabaseLocation;

        private List<User> nutzerDaten;
        public List<User> NutzerDaten
        {
            get { return nutzerDaten; }
            set { nutzerDaten = value; }
        }

        private List<Game> spielDaten;
        public List<Game> SpielDaten
        {
            get { return spielDaten; }
            set { spielDaten = value; }
        }

        private List<Link> verknüpfungen;
        public List<Link> Verknüpfungen
        {
            get { return verknüpfungen; }
            set { verknüpfungen = value; }
        }

        public XMLDatabase()
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Settings.CurrentSettings.XmlDatabaseLocation));

            nutzerDaten = new List<User>();
            spielDaten = new List<Game>();
            verknüpfungen = new List<Link>();
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
