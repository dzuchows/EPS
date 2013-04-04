using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi.model
{
    public class PECTestData
    {
        private string testNumber;
        private int test;
        private int step;
        private int cycle;
        private float totalTime;
        private float stepTime;
        private int cycleChannel;
        private int cycleDischargeTime;
        private float voltage;
        private float current;
        private float chargeCapacityAh;
        private float dischargeCapacityAh;
        private float chargeCapacityWh;
        private float dischargeCapacityWh;

        public string TestNumber
        {
            get { return testNumber; }
            set { testNumber = value; }
        }

        public int Test
        {
            get { return test; }
            set { test = value; }
        }

        public int Step
        {
            get { return step; }
            set { step = value; }
        }

        public int Cycle
        {
            get { return cycle; }
            set { cycle = value; }
        }

        public float TotalTime
        {
            get { return totalTime; }
            set { totalTime = value; }
        }

        public float StepTime
        {
            get { return stepTime; }
            set { stepTime = value; }
        }

        public int CycleChannel
        {
            get { return cycleChannel; }
            set { cycleChannel = value; }
        }

        public int CycleDischargeTime
        {
            get { return cycleDischargeTime; }
            set { cycleDischargeTime = value; }
        }

        public float Voltage
        {
            get { return voltage; }
            set { voltage = value; }
        }

        public float Current
        {
            get { return current; }
            set { current = value; }
        }

        public float ChargeCapacityAh
        {
            get { return chargeCapacityAh; }
            set { chargeCapacityAh = value; }
        }

        public float DischargeCapacityAh
        {
            get { return dischargeCapacityAh; }
            set { dischargeCapacityAh = value; }
        }

        public float ChargeCapacityWh
        {
            get { return chargeCapacityWh; }
            set { chargeCapacityWh = value; }
        }

        public float DischargeCapacityWh
        {
            get { return dischargeCapacityWh; }
            set { dischargeCapacityWh = value; }
        }

    }
}
