using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataUploadApi
{
    public class ModuleTest
    {
        private int testMachineId;

        public int TestMachineId
        {
            get { return testMachineId; }
            set { testMachineId = value; }
        }
        private int channelNum;

        public int ChannelNum
        {
            get { return channelNum; }
            set { channelNum = value; }
        }
        private string testType;

        public string TestType
        {
            get { return testType; }
            set { testType = value; }
        }
        private string testName;

        public string TestName
        {
            get { return testName; }
            set { testName = value; }
        }
        private DateTime file_timestamp;


        public DateTime File_timestamp
        {
            get { return file_timestamp; }
            set { file_timestamp = value; }
        }
        private DateTime upload_timestamp;

        public DateTime Upload_timestamp
        {
            get { return upload_timestamp; }
            set { upload_timestamp = value; }
        }
        private int parentId;

        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }
    }

}