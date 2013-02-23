using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadClient.Models
{
    public class ElectrodeWeight
    {
        private string bielectrodeNum;

        public string BielectrodeNum
        {
            get { return bielectrodeNum; }
            set { bielectrodeNum = value; }
        }
        private float positiveBipattiesWeight;

        public float PositiveBipattiesWeight
        {
            get { return positiveBipattiesWeight; }
            set { positiveBipattiesWeight = value; }
        }
        private float negativeBipattiesWeight;

        public float NegativeBipattiesWeight
        {
            get { return negativeBipattiesWeight; }
            set { negativeBipattiesWeight = value; }
        }
        private float gridWireWeight;

        public float GridWireWeight
        {
            get { return gridWireWeight; }
            set { gridWireWeight = value; }
        }
        private float precureBielectrodeWeight;

        public float PrecureBielectrodeWeight
        {
            get { return precureBielectrodeWeight; }
            set { precureBielectrodeWeight = value; }
        }
        private string operators;

        public string Operators
        {
            get { return operators; }
            set { operators = value; }
        }
        private DateTime timestamp;

        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }




    }
}
