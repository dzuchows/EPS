using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataUploader.Models
{
    public class UploadHistory
    {

        private string testName;
        private DateTime uploadTimestamp;
        private string status;


        public UploadHistory(string testName, DateTime uploadTimeStamp, string status) {
            this.testName = testName;
            this.uploadTimestamp = uploadTimestamp;
            this.status = status;
        }

        public string TestName
        {
            get { return testName; }
            set { testName = value; }
        }

        public DateTime UploadTimestamp
        {
            get { return uploadTimestamp; }
            set { uploadTimestamp = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }



    }
}