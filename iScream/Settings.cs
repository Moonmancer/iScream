using System.Data;

namespace iScream
{
    public static class Settings
    {
        internal static string SettingsXmlPath = @".\iScream.xml";

        private static string sqlInstance = ".";
        public static string SqlInstance
        {
            get { return sqlInstance; }
            set { sqlInstance = value; }
        }

        private static string sqlDatabaseName = "iScream";
        public static string SqlDatabaseName
        {
            get { return sqlDatabaseName; }
            set { sqlDatabaseName = value; }
        }

        private static bool sqlUseWinAuth = true;
        public static bool SqlUseWinAuth
        {
            get { return sqlUseWinAuth; }
            set { sqlUseWinAuth = value; }
        }


        private static string sqlUsername = Cryptography.Encrypt("sa");
        public static string SqlUsername
        {
            get { return sqlUsername; }
            set { sqlUsername = value; }
        }

        private static string sqlPassword;
        public static string SqlPassword
        {
            get { return sqlPassword; }
            set { sqlPassword = value; }
        }

        private static string xmlDatabaseLocation = System.IO.Path.Combine(System.IO.Path.GetTempPath(), @"iScream\XMLDatabase.xml");
        public static string XmlDatabaseLocation
        {
            get { return xmlDatabaseLocation; }
            set { xmlDatabaseLocation = value; }
        }

        public static void Save()
        {
            DataSet ds = new DataSet("iScream");

            DataTable dt = new DataTable("Settings");

            System.Reflection.PropertyInfo[] props = typeof(Settings).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

            foreach (System.Reflection.PropertyInfo pi in props)
                dt.Columns.Add(pi.Name);

            object[] data = new object[props.Length];

            for (int i = 0; i < props.Length; i++)
                if (props[i].Name != "XmlPath")
                    data[i] = typeof(Settings).GetProperty(props[i].Name, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null, null);

            dt.Rows.Add(data);
            ds.Tables.Add(dt);

            System.IO.File.WriteAllText(SettingsXmlPath, ds.GetXml());
        }

        public static void Load()
        {
            DataTable dt = null;
            string settingsString;

            try
            {
                settingsString = System.IO.File.ReadAllText(SettingsXmlPath);
                dt = ToDataTable(settingsString);
            }
            catch (System.IO.FileNotFoundException) { }
            catch (System.IO.DirectoryNotFoundException) { }

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        try
                        {
                            typeof(Settings).GetProperty(column.ColumnName, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).SetValue(null, row[column]);
                        }
                        catch { }
                    }
                }
            }
        }
        internal static DataTable ToDataTable(string settingsString)
        {
            DataSet ds = null;
            if (settingsString.Length > 0)
            {
                try
                {
                    ds = new DataSet();
                    System.IO.TextReader tr = System.IO.TextReader.Null;
                    settingsString = settingsString.Replace("\n", "");
                    settingsString = settingsString.Replace("\r", "");
                    System.Xml.XmlTextReader xmltr = new System.Xml.XmlTextReader(new System.IO.StringReader(settingsString));
                    ds.ReadXml(xmltr);
                }
                catch
                {
                    ds = null;
                }
            }

            return ds.Tables["Settings"];
        }
    }
}
