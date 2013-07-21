using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi.model
{
    public class PECTest : ModuleTest, ImpedanceTest
    {

        public PECTest(string testName)
        {
            this.TestName = testName;
            this.TestMachineId = 3;
            testResults = new Dictionary<string, List<PECTestData>>();
        }

        public Int16? NumberOfCells
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public Int16? RequestYear
        {
            get;
            set;
        }

        private string test;
        public string Test
        {
            get { return test; }
            set { test = value; }
        }

        private Dictionary<String, List<PECTestData>> testResults; 
        private string testRegimeName;
        private string testRegimeSuffix;
        private string testRegimeCellSize;
        private int? testRegimeVersion;
        private DateTime? startTime;
        private DateTime? endTime;

        public Dictionary<String, List<PECTestData>> TestResults
        {
            get { return testResults; }
            set { testResults = value; }
        }


        public string TestRegimeName
        {
            get { return testRegimeName; }
            set { testRegimeName = value; }
        }

        public string TestRegimeSuffix
        {
            get { return testRegimeSuffix; }
            set { testRegimeSuffix = value; }
        }

        public string TestRegimeCellSize
        {
            get { return testRegimeCellSize; }
            set { testRegimeCellSize = value; }
        }

        public int? TestRegimeVersion
        {
            get { return testRegimeVersion; }
            set { testRegimeVersion = value; }
        }

        public DateTime? StartTime
        {
            get
            {
                if (startTime.GetValueOrDefault().Year.Equals(1) && startTime.GetValueOrDefault().Month.Equals(1))
                {
                    return null;
                }
                else
                {
                    return startTime.GetValueOrDefault();                    
                }
            }
            set { startTime = value; }
        }

        public DateTime? EndTime
        {
            get
            {
                if (endTime.GetValueOrDefault().Year.Equals(1) && endTime.GetValueOrDefault().Month.Equals(1))
                {
                    return null;
                }
                else
                {
                    return endTime.GetValueOrDefault();
                }
            }
            set { endTime = value; }
        }

        #region ImpedanceTest Members

        IEnumerable<ImpedanceTestData> ImpedanceTest.getTestResults()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
