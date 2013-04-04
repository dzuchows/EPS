using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUploadApi.model;

namespace DataUploadApi.repository
{
    public class PECCSVDataSource : AbstractFileBasedDataSource<PECTest, PECTestData>
    {
        private string fileName;

        public PECCSVDataSource(string fileName)
        {
            this.fileName = fileName;
        }

        public PECTest getTestResults()
        {
            PECTest test = new PECTest(fileName);
            FileStream stream = null;
            StreamReader reader = null;
            try
            {

                stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(stream);
                extractTestHeader(reader, test);
                List<String> testNumbers = extractTestNumbers(Path.GetFileName(fileName));

                foreach (String s in testNumbers)
                {
                    test.TestResults.Add(s, new List<PECTestData>());
                }

                String line = returnFirstNonEmptyLine(reader);
                line = reader.ReadLine(); // header line
                line = reader.ReadLine(); // header line

                line = reader.ReadLine();
               
                while (!String.IsNullOrEmpty(line))
                {
                    int offset = 0;
                    foreach (String s in testNumbers)
                    {
                        test.TestResults[s].Add(extractTestResult(line, s, offset));
                        offset += 14;
                    }

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

        private void extractTestHeader(StreamReader reader, PECTest test)
        {
            String s = returnFirstNonEmptyLine(reader);

            test.Test = extractStringValue(s);
            test.TestRegimeName = extractStringValue(reader.ReadLine());
            test.TestRegimeSuffix = extractStringValue(reader.ReadLine());
            test.TestRegimeCellSize = extractStringValue(reader.ReadLine());
            test.TestRegimeVersion = extractIntValue(reader.ReadLine());
            test.StartTime = extractDateTimeValue(reader.ReadLine());
            test.EndTime = extractDateTimeValue(reader.ReadLine());
            
        }

        private PECTestData extractTestResult(String line, String testNumber, int offset)
        {
            var result = new PECTestData();

            String[] values = line.Split(',');
            try
            {
                result.TestNumber = testNumber;
                result.Test = getIntValue(values[offset]);
                result.Step = getIntValue(values[++offset]);
                result.Cycle = getIntValue(values[++offset]);

                result.TotalTime = getFloatValue(values[++offset]);
                result.StepTime = getFloatValue(values[++offset]);
                result.CycleChannel = getIntValue(values[++offset]);
                result.CycleDischargeTime = getIntValue(values[++offset]);
                result.Voltage = getFloatValue(values[++offset]);
                result.Current = getFloatValue(values[++offset]);
                result.ChargeCapacityAh = getFloatValue(values[++offset]);
                result.DischargeCapacityAh = getFloatValue(values[++offset]);
                result.ChargeCapacityWh = getFloatValue(values[++offset]);
                result.DischargeCapacityWh = getFloatValue(values[++offset]);

                return result;
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        private List<String> extractTestNumbers(String filename)
        {
            var testNumbers = new List<String>();

            filename = filename.Replace("EPS Gen1 Module_", "");

            var tokens = filename.Split('_');
            testNumbers.Add(tokens[0]);
            testNumbers.Add(tokens[1]);
            testNumbers.Add(tokens[2]);
            testNumbers.Add(tokens[3]);
            testNumbers.Add(tokens[4]);

            return testNumbers;
        }

    }
}
