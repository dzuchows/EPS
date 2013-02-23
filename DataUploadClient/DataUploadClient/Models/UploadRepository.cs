using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DataUploadClient.Models
{
    public class UploadRepository
    {

        public static IList<UploadHistory> getFiles(string path, string status)
        {
            IList<UploadHistory> history = new List<UploadHistory>();            

            DirectoryInfo di = new DirectoryInfo(path);
            var files = di.GetFiles();

            foreach(var file in files) {
                history.Add(new UploadHistory(file.Name, file.CreationTime, status, file.Name));
            }

            return history;
        }
    }
}