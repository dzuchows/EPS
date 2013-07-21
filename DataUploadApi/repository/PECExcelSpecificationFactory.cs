using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi.repository
{
    public class PECExcelSpecificationFactory
    {
        public static PECExcelSpecification getSpecification()
        {
            PECExcelSpecification specification = new PECExcelSpecification();

            specification.addHeaderField("Request Year:");
            specification.addHeaderField("Test:");
            specification.addHeaderField("Test Description:");
            specification.addHeaderField("TestRegime Name:");
            specification.addHeaderField("TestRegime Suffix:");
            specification.addHeaderField("TestRegime CellSize:");
            specification.addHeaderField("TestRegime Version:");
            specification.addHeaderField("Number of Cells:");
            specification.addHeaderField("LotID:");
            specification.addHeaderField("Lot Description:");
            specification.addHeaderField("Date Made:");
            specification.addHeaderField("Origin:");
            specification.addHeaderField("Requestor:");
            specification.addHeaderField("Product ID:");
            specification.addHeaderField("Storage Temp:");
            specification.addHeaderField("Storage Delay:");
            specification.addHeaderField("Test Temp:");
            specification.addHeaderField("Start Time:");
            specification.addHeaderField("End Time:");

            specification.addTestField("Test");
            specification.addTestField("Cell ID");
            specification.addTestField("Step");
            specification.addTestField("Cycle");
            specification.addTestField("Total Time (Seconds)");
            specification.addTestField("Step Time (Seconds)");
            specification.addTestField("Voltage (V)");
            specification.addTestField("Current (A)");
            specification.addTestField("Charge Capacity (mAh)");
            specification.addTestField("Discharge Capacity (mAh)");
            specification.addTestField("Charge Capacity (mWh)");
            specification.addTestField("Discharge Capacity (mWh)");

            return specification;
        }

    }
}
