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
    public class ElectrodeWeightGenealogyExcelDataSource
    {
        private string fileName;

        public ElectrodeWeightGenealogyExcelDataSource(string fileName)
        {
            this.fileName = fileName;
        }

        public IList<ElectrodeWeight> getWeightData()
        {             
            System.IO.FileStream stream = null;
            IExcelDataReader excelReader = null;
            IList<ElectrodeWeight> data = new List<ElectrodeWeight>();

            try
            {

                stream = File.Open(fileName, FileMode.Open, FileAccess.Read);

                excelReader = ElectrodeGenealogyExcelUtil.getExcelDataReader(fileName, stream);

                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();

                // load test data results
                DataRow row;
                ElectrodeWeight electrodeWeight;
                for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                {
                    electrodeWeight = new ElectrodeWeight();
                    row = result.Tables[0].Rows[i];
                    electrodeWeight.BielectrodeNum = Convert.ToString(row[0]);
                    electrodeWeight.PositiveBipattiesWeight = Convert.ToSingle(row[1]);
                    electrodeWeight.NegativeBipattiesWeight = Convert.ToSingle(row[2]);
                    electrodeWeight.GridWireWeight = Convert.ToSingle(row[3]);
                    electrodeWeight.PrecureBielectrodeWeight = Convert.ToSingle(row[4]);
                    electrodeWeight.Operators = Convert.ToString(row[5]);
                    electrodeWeight.Timestamp = DateTime.FromOADate(Convert.ToDouble(row[6]));

                    data.Add(electrodeWeight);
                }
            }
            finally
            {
                stream.Close();
                excelReader.Close();
            }

            return data;
        }
    }
}
