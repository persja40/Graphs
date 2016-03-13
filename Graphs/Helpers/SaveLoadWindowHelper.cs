using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Helpers
{
    public class SaveLoadWindowHelper
    {
        private static string ConfigPath
        {
            get
            {
                return Path.Combine(
                    AppdataHelper.AppDataDirectory,
                    "savepath.config"
                    );

            }
        }

        public static void SaveCurrentDialogDirectory(string path)
        {
            
            File.WriteAllText(ConfigPath, path);
        }

        public static string LoadCurrentDialogDirectory()
        {
            if(File.Exists(ConfigPath) == false)
            {
                SaveCurrentDialogDirectory(AppdataHelper.AppDataDirectory);
            }

            return File.ReadAllText(ConfigPath);
        }
    }
}
