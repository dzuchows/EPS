using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi
{
    public class FiringCircuitsTest : ModuleTest
    {
        private List<FiringCircuitsTestData> testResults;

        public List<FiringCircuitsTestData> TestResults
        {
            get { return testResults; }
            set { testResults = value; }
        }



        private int measurementId;

        public int MeasurementId
        {
            get { return measurementId; }
            set { measurementId = value; }
        }

        private String batteryName;

        public String BatteryName
        {
            get { return batteryName; }
            set { batteryName = value; }
        }

        private String circuit;

        public String Circuit
        {
            get { return circuit; }
            set { circuit = value; }
        }

        private String program;

        public String Program
        {
            get { return program; }
            set { program = value; }
        }

        private DateTime? startTime;

        public DateTime? StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        private DateTime? endTime;

        public DateTime? EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        private String testSection;

        public String TestSection
        {
            get { return testSection; }
            set { testSection = value; }
        }

        private String comment;

        public String Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private String orderNo;

        public String OrderNo
        {
            get { return orderNo; }
            set { orderNo = value; }
        }

        private String producer;

        public String Producer
        {
            get { return producer; }
            set { producer = value; }
        }

        private String type;

        public String Type
        {
            get { return type; }
            set { type = value; }
        }

        private float nominalVoltage;

        public float NominalVoltage
        {
            get { return nominalVoltage; }
            set { nominalVoltage = value; }
        }

        private float nominalCurrent;

        public float NominalCurrent
        {
            get { return nominalCurrent; }
            set { nominalCurrent = value; }
        }

        private float nominalCapacity;

        public float NominalCapacity
        {
            get { return nominalCapacity; }
            set { nominalCapacity = value; }
        }

        private float cells;

        public float Cells
        {
            get { return cells; }
            set { cells = value; }
        }

        private float maximumVoltage;

        public float MaximumVoltage
        {
            get { return maximumVoltage; }
            set { maximumVoltage = value; }
        }

        private float gassingVoltage;

        public float GassingVoltage
        {
            get { return gassingVoltage; }
            set { gassingVoltage = value; }
        }

        private float breakVoltage;

        public float BreakVoltage
        {
            get { return breakVoltage; }
            set { breakVoltage = value; }
        }

        private float chargeFactor;

        public float ChargeFactor
        {
            get { return chargeFactor; }
            set { chargeFactor = value; }
        }

        private float impedance;

        public float Impedance
        {
            get { return impedance; }
            set { impedance = value; }
        }

        private float coldCrankingCurrent;

        public float ColdCrankingCurrent
        {
            get { return coldCrankingCurrent; }
            set { coldCrankingCurrent = value; }
        }

        private float energyDensity;

        public float EnergyDensity
        {
            get { return energyDensity; }
            set { energyDensity = value; }
        }

    }
}
