using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DevReports
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DesignerForm());
        }
        public static string JsonSerializer<T>(T t, string jsonPanth)
        {
            File.WriteAllText(jsonPanth, JsonConvert.SerializeObject(t));
            return File.ReadAllText(jsonPanth);
        }


        public static T JsonDeserialize<T>(string jsonPanth)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(jsonPanth));
        }
    }
}
