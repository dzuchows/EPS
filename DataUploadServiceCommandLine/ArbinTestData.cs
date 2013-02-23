using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadService
{
    public class ArbinTestData
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int dataPoint;

        public int DataPoint
        {
            get { return dataPoint; }
            set { dataPoint = value; }
        }
        private float testTime;

        public float TestTime
        {
            get { return testTime; }
            set { testTime = value; }
        }
        private DateTime dateTime;

        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }
        private float stepTime;

        public float StepTime
        {
            get { return stepTime; }
            set { stepTime = value; }
        }
        private float stepIndex;

        public float StepIndex
        {
            get { return stepIndex; }
            set { stepIndex = value; }
        }
        private float cycleIndex;

        public float CycleIndex
        {
            get { return cycleIndex; }
            set { cycleIndex = value; }
        }
        private float current;

        public float Current
        {
            get { return current; }
            set { current = value; }
        }
        private float voltage;

        public float Voltage
        {
            get { return voltage; }
            set { voltage = value; }
        }
        private float power;

        public float Power
        {
            get { return power; }
            set { power = value; }
        }
        private float load;

        public float Load
        {
            get { return load; }
            set { load = value; }
        }
        private float chargeCapacity;

        public float ChargeCapacity
        {
            get { return chargeCapacity; }
            set { chargeCapacity = value; }
        }
        private float dischargeCapacity;

        public float DischargeCapacity
        {
            get { return dischargeCapacity; }
            set { dischargeCapacity = value; }
        }

        private float chargeEnergy;
        public float ChargeEnergy
        {
            get { return chargeEnergy; }
            set { chargeEnergy = value; }
        }

        private float dischargeEnergy;
        public float DischargeEnergy
        {
            get { return dischargeEnergy; }
            set { dischargeEnergy = value; }
        }

        private float dvdt;
        public float Dvdt
        {
            get { return dvdt; }
            set { dvdt = value; }
        }
        private float internalResistance;

        public float InternalResistance
        {
            get { return internalResistance; }
            set { internalResistance = value; }
        }
        private float isfcData;

        public float IsfcData
        {
            get { return isfcData; }
            set { isfcData = value; }
        }

        private float acimpedance;
        public float Acimpedance
        {
            get { return acimpedance; }
            set { acimpedance = value; }
        }
       
    }
}
