using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUploadApi.repository;
using NUnit.Framework;

namespace DataUploadApiTests
{
    [TestFixture]
    public class PECExcelSpecificationTests
    {
        protected PECExcelSpecification objectUnderTest = PECExcelSpecificationFactory.getSpecification();
        [TestCase]
        public void when_header_name_with_value_then_is_header_true()
        {
            String row = "TestRegime Name:,7.5Ah Proto 100per";

            Assert.That(objectUnderTest.isTestHeaderRow(row), Is.EqualTo(true));
        }

        [TestCase]
        public void when_header_name_from_test_data_then_is_header_false()
        {
            String row = "Test,Cell ID";

            Assert.That(objectUnderTest.isTestHeaderRow(row), Is.EqualTo(false));
        }

        [TestCase]
        public void when_header_name_from_Test_data_then_is_test_data_true()
        {
            String row = "Test,Cell ID,Step,Cycle,Total Time (Seconds),Step Time (Seconds),Voltage (V),Current (A),Charge Capacity (mAh),Discharge Capacity (mAh),Charge Capacity (mWh),Discharge Capacity (mWh),Test,Cell ID,Step,Cycle,Total Time (Seconds),Step Time (Seconds),Voltage (V),Current (A),Charge Capacity (mAh),Discharge Capacity (mAh),Charge Capacity (mWh),Discharge Capacity (mWh),Test,Cell ID,Step,Cycle,Total Time (Seconds),Step Time (Seconds),Voltage (V),Current (A),Charge Capacity (mAh),Discharge Capacity (mAh),Charge Capacity (mWh),Discharge Capacity (mWh),Test,Cell ID,Step,Cycle,Total Time (Seconds),Step Time (Seconds),Voltage (V),Current (A),Charge Capacity (mAh),Discharge Capacity (mAh),Charge Capacity (mWh),Discharge Capacity (mWh)";

            Assert.That(objectUnderTest.isTestDataRow(row), Is.EqualTo(true));
        }

    }
}
