using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataUploadClient.Models
{
    public class UploadHistory
    {

        private string testName;
        private DateTime uploadTimeStamp;
        private string status;
        private string fileName;


        public UploadHistory(string testName, DateTime uploadTimeStamp, string status, string fileName)
        {
            this.testName = testName;
            this.uploadTimeStamp = uploadTimeStamp;
            this.status = status;
            this.fileName = fileName;
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public string TestName
        {
            get { return testName; }
            set { testName = value; }
        }

        public DateTime UploadTimeStamp
        {
            get { return uploadTimeStamp; }
            set { uploadTimeStamp = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }



    }
}