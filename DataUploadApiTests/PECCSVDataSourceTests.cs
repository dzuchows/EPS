using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUploadApi.model;
using DataUploadApi.repository;
using NUnit.Framework;

namespace DataUploadApiTests
{
    [TestFixture]
    public class PECCSVDataSourceTests
    {

        [TestCase]
        public void can_read_firing_circuits_file()
        {
            PECCSVDataSource objectUnderTest =
                new PECCSVDataSource(@"C:\projects\EPS\EPS\TestData\EPS Gen1 Module_001_002_003_004_005_Form_Cyc.csv");
            PECTest test = objectUnderTest.getTestResults();

            Assert.IsTrue(!test.TestRegimeName.Equals(null));
            Assert.IsTrue(!test.TestRegimeCellSize.Equals(null));

        }

        [TestCase]
        public void can_save_to_database()
        {
            PECCSVDataSource objectUnderTest =
                new PECCSVDataSource(@"C:\projects\EPS\EPS\TestData\EPS Gen1 Module_001_002_003_004_005_Form_Cyc.csv");
            PECTest test = objectUnderTest.getTestResults();

            PECTestDataRepository repository = new PECTestDataRepository();

            repository.save(test);
        }
    }
}
