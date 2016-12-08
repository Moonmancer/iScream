using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream.GUI.Classes
{
    class GUIStartup
    {
        private IFachkonzept _fachkonzept;

        public GUIStartup(IFachkonzept fachkonzept)
        {
            _fachkonzept = fachkonzept;
        }

        public void RunGUI ()
        {
            GUIMain GUI = new GUIMain();
            GUI.Activate();
        }
    }
}
