using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataUploadClient.Models
{
    public class ArbinTest : ModuleTest
    {

        private IList<ArbinTestData> testResults;

        public IList<ArbinTestData> TestResults
        {
            get { return testResults; }
            set { testResults = value; }
        }

        public ArbinTest()
        {
            this.TestMachineId = 1;
            testResults = new List<ArbinTestData>();
        }

    }
}