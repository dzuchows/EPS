using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel;

namespace DataUploadApi
{
    public class ElectrodeGenealogyExcelUtil
    {

        public static IExcelDataReader getExcelDataReader(string fileName, FileStream stream)
        {
            if (fileName.Contains(".xlsx"))
            {
                return ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else if (fileName.Contains(".xls"))
            {
                return ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else
            {
                throw new ArgumentException(String.Format("Invalid fileName: {0}", fileName));
            }
        }

    }
}
