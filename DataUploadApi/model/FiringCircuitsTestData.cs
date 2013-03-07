using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi
{
    public class FiringCircuitsTestData
    {
        private DateTime? timeStamp;

        public DateTime? TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        private int step;

        public int Step
        {
            get { return step; }
            set { step = value; }
        }

        private String status;

        public String Status
        {
            get { return status; }
            set { status = value; }
        }

        private String progTime;

        public String ProgTime
        {
            get { return progTime; }
            set { progTime = value; }
        }

        private String stepTime;

        public String StepTime
        {
            get { return stepTime; }
            set { stepTime = value; }
        }

        private int cycle;

        public int Cycle
        {
            get { return cycle; }
            set { cycle = value; }
        }

        private int cycleLevel;

        public int CycleLevel
        {
            get { return cycleLevel; }
            set { cycleLevel = value; }
        }

        private String procedure;

        public String Procedure
        {
            get { return procedure; }
            set { procedure = value; }
        }

        private float voltage;

        public float Voltage
        {
            get { return voltage; }
            set { voltage = value; }
        }

        private float currentA;

        public float CurrentA
        {
            get { return currentA; }
            set { currentA = value; }
        }

        private float ahAccu;

        public float AhAccu
        {
            get { return ahAccu; }
            set { ahAccu = value; }
        }

        private float ahCha;

        public float AhCha
        {
            get { return ahCha; }
            set { ahCha = value; }
        }

        private float ahDch;

        public float AhDch
        {
            get { return ahDch; }
            set { ahDch = value; }
        }

        private float ahStep;

        public float AhStep
        {
            get { return ahStep; }
            set { ahStep = value; }
        }

        private float energy;

        public float Energy
        {
            get { return energy; }
            set { energy = value; }
        }

        private float whStep;

        public float WhStep1
        {
            get { return whStep; }
            set { whStep = value; }
        }

        public float WhStep
        {
            get { return whStep; }
            set { whStep = value; }
        }
    }
}
