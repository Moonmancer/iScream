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

        User HoleNutzer(int nutzer_id);
        List<User> HoleNutzer();

        Game HoleSpiel(int spiel_id);
        List<Game> HoleSpiel();

        List<Link> HoleVerknüpfung();

        List<Game> HoleSpieleVonNutzer(int nutzer_id);
        List<Game> HoleSpieleVonNutzer(User nutzer);

        List<User> HoleNutzerVonSpiel(int spiel_id);
        List<User> HoleNutzerVonSpiel(Game spiel);
        #endregion

        #region Hinzufügen
        bool FügeNutzerHinzu(string vorname, string nachname, int nutzer_id);
        bool FügeNutzerHinzu(User nutzer);
        void FügeNutzerHinzu(List<User> nutzer);

        bool FügeSpielHinzu(string name, int spiel_id);
        bool FügeSpielHinzu(Game spiel);
        void FügeSpielHinzu(List<Game> spiele);

        bool FügeVerknüpfungHinzu(int nutzer_id, int spiel_id);
        bool FügeVerknüpfungHinzu(Link verknüpfung);
        void FügeVerknüpfungHinzu(List<Link> verknüpfungen);
        #endregion

        #region Löschen
        #endregion

        #region Ändern
        #endregion
    }
}
