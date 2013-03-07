using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi
{
    public class FiringCircuitsCSVDataSource : FileBasedDataSource<FiringCircuitsTest, FiringCircuitsTestData>
    {
        private string fileName;

        public FiringCircuitsCSVDataSource(string fileName)
        {
            this.fileName = fileName;
        }

        public FiringCircuitsTest getTestResults()
        {
            FiringCircuitsTest test = new FiringCircuitsTest();

            try
            {

                var stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                var reader = new StreamReader(stream);



            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void extractTestHeader(StreamReader reader, FiringCircuitsTest test)
        {
            reader = returnFirstNonEmptyLine(reader);

            test.MeasurementId = extractIntValue(reader);
            test.BatteryName = extractStringValue(reader);
            test.BatteryName = extractStringValue(reader);
            test.Circuit = extractStringValue(reader);
            test.Program = extractStringValue(reader);
            test.StartTime = extractDateTimeValue(reader);
            test.EndTime = extractDateTimeValue(reader);
            test.Comment = extractStringValue(reader);
            test.OrderNo = extractStringValue(reader);
            test.Producer = extractStringValue(reader);
            test.Type = extractStringValue(reader);
            test.NominalVoltage = extractFloatValue(reader);
            test.NominalCurrent = extractFloatValue(reader);
            test.NominalCapacity = extractFloatValue(reader);
            test.Cells = extractFloatValue(reader);
            test.MaximumVoltage = extractFloatValue(reader);
            test.GassingVoltage = extractFloatValue(reader);
            test.BreakVoltage = extractFloatValue(reader);
            test.ChargeFactor = extractFloatValue(reader);
            test.Impedance = extractFloatValue(reader);
            test.ColdCrankingCurrent = extractFloatValue(reader);
            test.EnergyDensity = extractFloatValue(reader);
            test.Comment = extractStringValue(reader);
        }

        private FiringCircuitsTestData extractTestResult(StreamReader reader)
        {
            var result = new FiringCircuitsTestData();

            result.TimeStamp = extractDateTimeValue(reader);
            result.Step = extractIntValue(reader);
            result.Status = extractStringValue(reader);
            result.ProgTime = extractStringValue(reader);
            result.StepTime = extractStringValue(reader);
            result.Cycle = extractIntValue(reader);
            result.CycleLevel = extractIntValue(reader);
            result.Procedure = extractStringValue(reader);
            result.Voltage = extractFloatValue(reader);
            result.CurrentA = extractFloatValue(reader);
            result.AhAccu = extractFloatValue(reader);



            return result;
        }



        private StreamReader returnFirstNonEmptyLine(StreamReader reader)
        {
            string s;
            while ((s = reader.ReadLine()).Length == 0) ;
            return reader;
        }

        private int extractIntValue(StreamReader reader)
        {
            string[] s;
            s = reader.ReadLine().Split(',');
            if (!String.IsNullOrEmpty(s[1])) return Convert.ToInt32(s[1]);
            return 0;
        }

        private string extractStringValue(StreamReader reader)
        {
            string[] s;
            s = reader.ReadLine().Split(',');
            return s[1];
        }

        private DateTime? extractDateTimeValue(StreamReader reader)
        {
            string[] s;
            s = reader.ReadLine().Split(',');
            if (!String.IsNullOrEmpty(s[1]))  return Convert.ToDateTime(s[1]);
            return null; 
        }

        private float extractFloatValue(StreamReader reader)
        {
            string[] s;
            s = reader.ReadLine().Split(',');
            if (!String.IsNullOrEmpty(s[1])) return Convert.ToSingle(s[1]);
            return 0.0F;
        }

    }
}
