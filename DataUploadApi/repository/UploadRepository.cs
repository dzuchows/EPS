﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DataUploadApi
{
    public class UploadRepository
    {

        public static IList<UploadHistory> getFiles(string path, string status, string testType)
        {
            IList<UploadHistory> history = new List<UploadHistory>();            

            DirectoryInfo di = new DirectoryInfo(path);
            var files = di.GetFiles();

            foreach(var file in files) {
                history.Add(new UploadHistory(file.Name, file.CreationTime, status, file.Name, testType));
            }

            return history;
        }
    }
}