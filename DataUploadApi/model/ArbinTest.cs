using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUploadApi.model;

namespace DataUploadApi
{
    public class ArbinTest : ModuleTest, ImpedanceTest
    {

        private IList<ArbinTestData> testResults;

        public IList<ArbinTestData> TestResults
        {
            get { return testResults; }
            set { testResults = value; }
        }

        public ArbinTest()
        {
            TestMachineId = 1;
            testResults = new List<ArbinTestData>();
        }

        IEnumerable<ImpedanceTestData> ImpedanceTest.getTestResults()
        {
            return testResults;
        }

    }
}
