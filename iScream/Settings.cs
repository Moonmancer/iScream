using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace iScream
{
    public class Settings
    {
        private static Settings currentSettings;
        [XmlIgnore]
        public static Settings CurrentSettings
        {
            get
            {
                if (currentSettings == null)
                    return Load();
                return currentSettings;
            }
            set { currentSettings = value; }
        }

        #region Datenhaltung1
        private string sqlDatabaseName = "iScream";
        public string SqlDatabaseName
        {
            get { return sqlDatabaseName; }
            set { sqlDatabaseName = value; }
        }

        private string sqlServerLocation = ".";
        public string SqlServerLocation
        {
            get { return sqlServerLocation; }
            set { sqlServerLocation = value; }
        }

        private bool useWinAuth = true;
        public bool UseWinAuth
        {
            get { return useWinAuth; }
            set { useWinAuth = value; }
        }

        private string sqlServerUsername = Kryptographie.Verschlüsseln("sa");
        /// <summary>
        /// Dieser Wert sollte mit der Kryptogrphie-Klasse Ver- und Entschlüsselt werden!
        /// </summary>
        public string SqlServerUsername
        {
            get { return sqlServerUsername; }
            set { sqlServerUsername = value; }
        }

        private string sqlServerPassword;
        /// <summary>
        /// Dieser Wert sollte mit der Kryptogrphie-Klasse Ver- und Entschlüsselt werden!
        /// </summary>
        public string SqlServerPassword
        {
            get { return sqlServerPassword; }
            set { sqlServerPassword = value; }
        }
        #endregion

        #region Datenhaltung2
        private string xmlDatabaseLocation = Path.Combine(Path.GetTempPath(), "iScream\\XMLDatabase.xml");
        public string XmlDatabaseLocation
        {
            get { return xmlDatabaseLocation; }
            set { xmlDatabaseLocation = value; }
        }
        #endregion

        public event EventHandler<EventArgs> SettingsChanged;

        public Settings() { }

        public static Settings Load()
        {
            try
            {
                using (FileStream fs = new FileStream("iScream.cfg", FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Settings));
                    return (Settings)xs.Deserialize(fs);
                }
            }
            catch (FileNotFoundException ex)
            {
                return new Settings();
            }
        }

        public void Save()
        {
            CurrentSettings = this;
            using (FileStream fs = new FileStream("iScream.cfg", FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Settings));
                xs.Serialize(fs, this);
            }

        }
    }
}
