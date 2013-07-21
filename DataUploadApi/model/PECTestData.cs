using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi.model
{
    public class PECTestData : ImpedanceTestData
    {

        public string TestNumber
        {
            get;
            set;
        }

        public int Test
        {
            get;
            set;
        }

        public int Step
        {
            get;
            set;
        }

        public int Cycle
        {
            get;
            set;
        }

        public float TotalTime
        {
            get;
            set;
        }

        public float StepTime
        {
            get;
            set;
        }

        public int CycleChannel
        {
            get;
            set;
        }

        public int CycleDischargeTime
        {
            get;
            set;
        }

        public float Voltage
        {
            get;
            set;
        }

        public float Current
        {
            get;
            set;
        }

        public float ChargeCapacityAh
        {
            get;
            set;
        }

        public float DischargeCapacityAh
        {
            get;
            set;
        }

        public float ChargeCapacityWh
        {
            get;
            set;
        }

        public float DischargeCapacityWh
        {
            get;
            set;
        }


        #region ImpedanceTestData Members

        float ImpedanceTestData.StepIndex
        {
            get { return Step; }
        }

        float ImpedanceTestData.CycleIndex
        {
            get { return Cycle; }
        }

        float ImpedanceTestData.Current
        {
            get { return Current; }
        }

        float ImpedanceTestData.Voltage
        {
            get { return Voltage; }
        }

        float ImpedanceTestData.ChargeCapacity
        {
            get { return ChargeCapacityAh; }
        }

        float ImpedanceTestData.DischargeCapacity
        {
            get { return DischargeCapacityAh;  }
        }

        #endregion
    }
}
