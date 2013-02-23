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
    public class ElectrodeThicknessGenealogyExcelDataSource
    {
        private string fileName;

        public ElectrodeThicknessGenealogyExcelDataSource(string fileName)
        {
            this.fileName = fileName;
        }

        public IList<ElectrodeThickness> getThicknessData()
        {             
            System.IO.FileStream stream = null;
            IExcelDataReader excelReader = null;
            IList<ElectrodeThickness> data = new List<ElectrodeThickness>();

            try
            {

                stream = File.Open(fileName, FileMode.Open, FileAccess.Read);

                excelReader = ElectrodeGenealogyExcelUtil.getExcelDataReader(fileName, stream);

                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();

                // load test data results
                DataRow row;
                ElectrodeThickness electrodeThickness;
                for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                {
                    electrodeThickness = new ElectrodeThickness();
                    row = result.Tables[0].Rows[i];
                    electrodeThickness.BielectrodeNum = Convert.ToString(row[0]);
                    electrodeThickness.Thickness_1 = Convert.ToSingle(row[1]);
                    electrodeThickness.Thickness_2 = Convert.ToSingle(row[2]);
                    electrodeThickness.Thickness_3 = Convert.ToSingle(row[3]);

                    electrodeThickness.Thickness_4 = Convert.ToSingle(row[4]);
                    electrodeThickness.Thickness_5 = Convert.ToSingle(row[5]);
                    electrodeThickness.Thickness_6 = Convert.ToSingle(row[6]);

                    electrodeThickness.Thickness_7 = Convert.ToSingle(row[7]);
                    electrodeThickness.Thickness_8 = Convert.ToSingle(row[8]);
                    electrodeThickness.Thickness_9 = Convert.ToSingle(row[9]);

                    data.Add(electrodeThickness);
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
