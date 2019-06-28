using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AppVLO.Services;
using Foundation;
using UIKit;

namespace AppVLO.iOS.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            string docFolder =
                Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder =
                Path.Combine(docFolder, "..", "Databases");
            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, fileName);
        }
    }
}