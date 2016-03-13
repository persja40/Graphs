using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Helpers
{
    public class AppdataHelper
    {
        static AppdataHelper()
        {
            createAppdataFolder();
        }

        public static string AppDataDirectory
        {
            get
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                string myFolder = System.IO.Path.Combine(folder, "AghGraphs");

                return myFolder;
            }
        }

        private static void createAppdataFolder()
        {
            if (!Directory.Exists(AppDataDirectory))
                Directory.CreateDirectory(AppDataDirectory);
        }
    }
}
