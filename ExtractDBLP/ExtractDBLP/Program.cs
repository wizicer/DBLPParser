using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtractDBLPForm
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmDBLPExtract());

        }

        static void CleanCcf()
        {
            var json = File.ReadAllText("ccf.json");
            var list = JsonConvert.DeserializeObject<CcfRecord[]>(json);
            foreach (var item in list)
            {
                if (item.CrossRef == null || item.CrossRef == "NULL")
                {
                    item.CrossRef = null;
                }
                else if (!item.CrossRef.EndsWith("/"))
                {
                    item.CrossRef += "/";
                }

                if (item.Title == "NULL") item.Title = null;
                if (item.Publisher == "NULL") item.Publisher = null;
            }

            json = JsonConvert.SerializeObject(list, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            File.WriteAllText(@"..\..\ccf.json", json);
        }
    }
}
