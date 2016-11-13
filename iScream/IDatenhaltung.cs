using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    interface IDatenhaltung
    {
        #region Holen
        int HoleNächsteNutzer_id();
        int HoleNächsteSpiel_id();

        Nutzer HoleNutzer(int nutzer_id);
        List<Nutzer> HoleNutzer();

        Spiel HoleSpiel(int spiel_id);
        List<Spiel> HoleSpiel();

        Verknüpfung HoleVerknüpfung(int nutzer_id, int spiel_id);
        Verknüpfung HoleVerknüpfung(Verknüpfung verknüpfung);
        List<Verknüpfung> HoleVerknüpfung();

        List<Spiel> HoleSpieleVonNutzer(int nutzer_id);
        List<Spiel> HoleSpieleVonNutzer(Nutzer nutzer);

        List<Nutzer> HoleNutzerVonSpiel(int spiel_id);
        List<Nutzer> HoleNutzerVonSpiel(Spiel spiel);
        #endregion

        #region Hinzufügen
        bool FügeNutzerHinzu(string vorname, string nachname, int nutzer_id);
        bool FügeNutzerHinzu(string vorname, string nachname);
        bool FügeNutzerHinzu(Nutzer nutzer);
        void FügeNutzerHinzu(List<Nutzer> nutzer);

        bool FügeSpielHinzu(string name, int spiel_id);
        bool FügeSpielHinzu(string name);
        bool FügeSpielHinzu(Spiel spiel);
        void FügeSpielHinzu(List<Spiel> spiele);

        bool FügeVerknüpfungHinzu(int nutzer_id, int spiel_id);
        bool FügeVerknüpfungHinzu(Verknüpfung verknüpfung);
        void FügeVerknüpfungHinzu(List<Verknüpfung> verknüpfungen);
        #endregion

        #region Löschen
        bool LöscheNutzer(int nutzer_id);
        bool LöscheNutzer(Nutzer nutzer);
        void LöscheNutzer(List<Nutzer> nutzer);

        bool LöscheSpiel(int spiel_id);
        bool LöscheSpiel(Spiel spiel);
        void LöscheSpiel(List<Spiel> spiele);

        bool LöscheVerknüpfung(int nutzer_id, int spiel_id);
        bool LöscheVerknüpfung(Verknüpfung verknüpfung);
        #endregion

        #region Ändern
        bool ÄndereNutzer(int nutzer_id, string vorname, string nachname);
        bool ÄndereNutzer(Nutzer nutzer);

        bool ÄndereSpiel(int spiel_id, string name);
        bool ÄndereSpiel(Spiel spiel);
        #endregion
    }
}
