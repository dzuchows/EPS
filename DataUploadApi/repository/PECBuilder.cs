using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUploadApi.model;

namespace DataUploadApi.repository
{
    public class PECBuilder
    {
        private PECTest test;

        public void newTest(string testName)
        {
            test = new PECTest(testName);
            return test;
        }

        private static String removeHeaderDecoration(string header) 
        {
            return header.Replace(":", "");
        }

        public void addHeaderField(string name, string value)
        {
            name = removeHeaderDecoration(name);

            switch (name)
            {
                case "Request Year:":
                    test.RequestYear = Convert.ToInt16(value);
                    break;

                case "Test:":
                    test.Test = value;
                    break;

                case "Test Description:":
                    test.Description = value;
                    break;

                case "TestRegime Name:":
                    test.TestRegimeName = value;
                    break;

                case "TestRegime Suffix:":
                    test.TestRegimeSuffix = value;
                    break;

                case "TestRegime CellSize:":
                    test.TestRegimeCellSize = value;
                    break;

                case "TestRegime Version:":
                    test.TestRegimeVersion = Convert.ToInt16(value);
                    break;

                case "Number of Cells:":
                    test.NumberOfCells = Convert.ToInt16(value);
                    break;

                case "LotID:":
                case "Lot Description:":
                case "Date Made:":
                case "Origin:":
                case "Requestor:":
                case "Product ID:":
                case "Storage Temp:":
                case "Storage Delay:":
                case "Test Temp:":
                case "Start Time:":
                case "End Time:":
                    break;

            };
        }

        public PECTest build()
        {
            return test;
        }
    }
}
