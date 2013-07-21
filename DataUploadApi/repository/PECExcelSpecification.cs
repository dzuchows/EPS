using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi.repository
{
    public class PECExcelSpecification
    {
        private List<String> headerFields = new List<String>();
        private List<String> testDataFields = new List<String>();

        public void addHeaderField(String headerField) 
        {
            if (!headerFields.Contains(headerField)) 
            {
                headerFields.Add(headerField);
            }
        }

        public void removeHeaderField(String headerField) 
        {
            headerFields.Remove(headerField);  
        }

        public bool isHeaderField(String fieldName)
        {
            return headerFields.Contains(fieldName);
        }

        public void addTestField(String fieldName)
        {
            if (!testDataFields.Contains(fieldName))
            {
                testDataFields.Add(fieldName);
            }
        }

        public void removeTestField(String fieldName)
        {
            testDataFields.Remove(fieldName);
        }

        public bool isTestDataField(String fieldName)
        {
            return testDataFields.Contains(fieldName);
        }

        public bool isTestHeaderRow(String row)
        {
            List<String> fields = row.Split(',').ToList();

            if (fields.Count < 2)
                return false;

            if (isHeaderField(fields[0]) && !isTestDataField(fields[0]))
            {
                return true;
            } else if (isHeaderField(fields[0]) && !isTestDataField(fields[1])) {
                return true;
            }
            return false;
        }

        public bool isTestDataRow(String row)
        {
            List<String> fields = row.Split(',').ToList();

            foreach(String f in fields ) {
                if ( !isTestDataField(f)) {
                    return false;
                }
            }
            return true;
        }
    }
}
