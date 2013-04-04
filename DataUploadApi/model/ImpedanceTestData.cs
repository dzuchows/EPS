using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi.model
{
    interface ImpedanceTestData
    {
        public int StepIndex;

        public int CycleIndex;
        
        public float Current;

        public float Voltage;
    }
}
