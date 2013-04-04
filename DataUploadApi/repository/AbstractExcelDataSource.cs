using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi
{
    public abstract class AbstractExcelDataSource<T, R>
    {
        protected string getCellValue(DataTable table, int row, int column)
        {
            if (!DBNull.Value.Equals(table.Rows[row][column]))
            {
                return (string)table.Rows[row][column];
            }
            else
            {
                return "";
            }
        }

        protected int getTableIndexByName(DataSet dataSet, string tableName)
        {
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                if (dataSet.Tables[i].TableName.Contains(tableName))
                {
                    return i;
                }
            }
            return -1;
        }

        protected int getTestResultColumnIndex(DataTable sheet, string columnName)
        {
            for (int i = 0; i < sheet.Rows.Count; i++)
            {
                if (!DBNull.Value.Equals(sheet.Rows[i][5]))
                {
                    if (columnName.Equals(sheet.Rows[i][5]))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

    }
}
