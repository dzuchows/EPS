using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadClient.Models
{
    public class ElectrodeGenealogySummary
    {
        string bielectrodeId;

        public string BielectrodeId
        {
            get { return bielectrodeId; }
            set { bielectrodeId = value; }
        }
        string hasWeightData;

        public string HasWeightData
        {
            get { return hasWeightData; }
            set { hasWeightData = value; }
        }
        string hasThicknessData;

        public string HasThicknessData
        {
            get { return hasThicknessData; }
            set { hasThicknessData = value; }
        }
    }
}
