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

        List<Verknüpfung> HoleVerknüpfung();

        List<Spiel> HoleSpieleVonNutzer(int nutzer_id);
        List<Spiel> HoleSpieleVonNutzer(Nutzer nutzer);

        List<Nutzer> HoleNutzerVonSpiel(int spiel_id);
        List<Nutzer> HoleNutzerVonSpiel(Spiel spiel);
        #endregion

        #region Hinzufügen
        bool FügeNutzerHinzu(string vorname, string nachname, int nutzer_id);
        bool FügeNutzerHinzu(Nutzer nutzer);
        void FügeNutzerHinzu(List<Nutzer> nutzer);

        bool FügeSpielHinzu(string name, int spiel_id);
        bool FügeSpielHinzu(Spiel spiel);
        void FügeSpielHinzu(List<Spiel> spiele);

        bool FügeVerknüpfungHinzu(int nutzer_id, int spiel_id);
        bool FügeVerknüpfungHinzu(Verknüpfung verknüpfung);
        void FügeVerknüpfungHinzu(List<Verknüpfung> verknüpfungen);
        #endregion

        #region Löschen
        #endregion

        #region Ändern
        #endregion
    }
}
