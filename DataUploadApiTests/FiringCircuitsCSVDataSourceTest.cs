using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUploadApi;
using DataUploadApi.repository;
using NUnit.Framework;

namespace DataUploadApiTests
{
    [TestFixture]
    public class FiringCircuitsCSVDataSourceTest
    {

        [TestCase]
        public void can_read_firing_circuits_file()
        {
            FiringCircuitsCSVDataSource objectUnderTest = new FiringCircuitsCSVDataSource(@"C:\projects\EPS\EPS\TestData\Firing_Circuits.csv");
            FiringCircuitsTest test = objectUnderTest.getTestResults();

            Assert.IsTrue(!test.BatteryName.Equals(null));
            Assert.IsTrue(!test.TestResults.Equals(null));

            objectUnderTest = new FiringCircuitsCSVDataSource(@"C:\projects\EPS\EPS\TestData\discover03-C3 100%.csv");
            test = objectUnderTest.getTestResults();

            Assert.IsTrue(!test.BatteryName.Equals(null));
            Assert.IsTrue(!test.TestResults.Equals(null));

            objectUnderTest = new FiringCircuitsCSVDataSource(@"C:\projects\EPS\EPS\TestData\Enersys01 3x C10 Cond.csv");
            test = objectUnderTest.getTestResults();

            Assert.IsTrue(!test.BatteryName.Equals(null));
            Assert.IsTrue(!test.TestResults.Equals(null));

        }

        [TestCase]
        public void can_save_firing_circuits()
        {
            FiringCircuitsCSVDataSource objectUnderTest = new FiringCircuitsCSVDataSource(@"C:\projects\EPS\EPS\TestData\Firing_Circuits.csv");
            FiringCircuitsTest test = objectUnderTest.getTestResults();
            FiringCircuitsTestDataRepository repository = new FiringCircuitsTestDataRepository();

            repository.save(test);
        }
    }
}
