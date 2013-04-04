using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi
{
    public class FiringCircuitsCSVDataSource : AbstractFileBasedDataSource<FiringCircuitsTest, FiringCircuitsTestData>
    {
        private string fileName;

        public FiringCircuitsCSVDataSource(string fileName)
        {
            this.fileName = fileName;
        }

        public FiringCircuitsTest getTestResults()
        {
            FiringCircuitsTest test = new FiringCircuitsTest();
            FileStream stream = null;
            StreamReader reader = null;
            try
            {

                stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(stream);

                extractTestHeader(reader, test);

                String line = returnFirstNonEmptyLine(reader);
                line = reader.ReadLine(); // header line
                line = reader.ReadLine(); // header line

                line = reader.ReadLine();
                while (!String.IsNullOrEmpty(line))
                {
                    test.TestResults.Add(extractTestResult(line));
                    line = reader.ReadLine();
                }

                return test;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                stream.Close();
                reader.Close();
            }
        }

        private void extractTestHeader(StreamReader reader, FiringCircuitsTest test)
        {
            String s = returnFirstNonEmptyLine(reader);

            test.MeasurementId = extractIntValue(s);
            test.BatteryName = extractStringValue(reader.ReadLine());
            test.BatteryName = extractStringValue(reader.ReadLine());
            test.Circuit = extractStringValue(reader.ReadLine());
            test.Program = extractStringValue(reader.ReadLine());
            test.StartTime = extractDateTimeValue(reader.ReadLine());
            test.EndTime = extractDateTimeValue(reader.ReadLine());
            test.TestSection = extractStringValue(reader.ReadLine());
            test.Comment = extractStringValue(reader.ReadLine());
            test.OrderNo = extractStringValue(reader.ReadLine());
            test.Producer = extractStringValue(reader.ReadLine());
            test.Type = extractStringValue(reader.ReadLine());
            test.NominalVoltage = extractFloatValue(reader.ReadLine());
            test.NominalCurrent = extractFloatValue(reader.ReadLine());
            test.NominalCapacity = extractFloatValue(reader.ReadLine());
            test.Cells = extractFloatValue(reader.ReadLine());
            test.MaximumVoltage = extractFloatValue(reader.ReadLine());
            test.GassingVoltage = extractFloatValue(reader.ReadLine());
            test.BreakVoltage = extractFloatValue(reader.ReadLine());
            test.ChargeFactor = extractFloatValue(reader.ReadLine());
            test.Impedance = extractFloatValue(reader.ReadLine());
            test.ColdCrankingCurrent = extractFloatValue(reader.ReadLine());
            test.EnergyDensity = extractFloatValue(reader.ReadLine());
            test.Comment = extractStringValue(reader.ReadLine());

            test.TestName = test.BatteryName + " - " + test.MeasurementId;
        }

        private FiringCircuitsTestData extractTestResult(String line)
        {
            var result = new FiringCircuitsTestData();

            String[] values = line.Split(',');

            result.TimeStamp = getDateTimeValue(values[0]);
            result.Step = getIntValue(values[1]);
            result.Status = values[2];
            result.ProgTime = values[3];
            result.StepTime = values[4];
            result.Cycle = getIntValue(values[5]);
            result.CycleLevel = getIntValue(values[6]);
            result.Procedure = values[7];
            result.Voltage = getFloatValue(values[8]);
            result.CurrentA = getFloatValue(values[9]);
            result.AhAccu = getFloatValue(values[10]);

            return result;
        }

        
       
    }
}
