using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel;

namespace DataUploadApi
{
    public class ArbinExcelDataSource : AbstractExcelDataSource<ArbinTest, ArbinTestData>, FileBasedDataSource<ArbinTest, ArbinTestData>
    {
        private string fileName;

        public ArbinExcelDataSource(string fileName)
        {
            this.fileName = fileName;
        }

        public ArbinTest getTestResults()
        {
            ArbinTest test = new ArbinTest();
            System.IO.FileStream stream = null;
            IExcelDataReader excelReader = null;
            try
            {

                stream = File.Open(fileName, FileMode.Open, FileAccess.Read);

                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();
                int infoTableIndex = getTableIndexByName(result, "Info");

                DataTable testInfo = result.Tables[infoTableIndex];
                test.ChannelNum = Int32.Parse(getCellValue(testInfo, getTestResultColumnIndex(result.Tables[infoTableIndex], "Chan_Num") + 1, 5));  
                test.TestName = getCellValue(testInfo, 0, 3);
                    
                // load test data results
                DataRow row;
                ArbinTestData data;
                int resultsTableIndex = getTableIndexByName(result, "Channel_1");
                for (int i = 0; i < result.Tables[resultsTableIndex].Rows.Count; i++)
                {
                    data = new ArbinTestData();
                    row = result.Tables[resultsTableIndex].Rows[i];
                    data.DataPoint = Convert.ToInt32(row[0]);
                    data.TestTime = Convert.ToSingle(row[1]);
                    data.DateTime = DateTime.FromOADate(Convert.ToDouble(row[5]));
                    data.StepTime = Convert.ToSingle(row[6]);
                    data.StepIndex = Convert.ToInt32(row[7]);
                    data.CycleIndex = Convert.ToInt32(row[8]);
                    data.Current = Convert.ToSingle(row[9]);
                    data.Voltage = Convert.ToSingle(row[11]);
                    data.Power = Convert.ToSingle(row[13]);
                    data.Load = Convert.ToSingle(row[14]);
                    data.ChargeCapacity = Convert.ToSingle(row[15]);
                    data.DischargeCapacity = Convert.ToSingle(row[16]);
                    data.ChargeEnergy = Convert.ToSingle(row[17]);
                    data.DischargeEnergy = Convert.ToSingle(row[18]);
                    data.Dvdt = Convert.ToSingle(row[19]);
                    data.InternalResistance = Convert.ToSingle(row[20]);
                    data.IsfcData = Convert.ToSingle(row[21]);
                    data.Acimpedance = Convert.ToSingle(row[22]);

                    test.TestResults.Add(data);
                }
            }
            finally
            {
                if ( stream != null) stream.Close();
                if ( excelReader != null) excelReader.Close();
            }

            return test;
        }
    }
}
