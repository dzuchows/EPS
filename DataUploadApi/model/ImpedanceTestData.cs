using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi.model
{
    public interface ImpedanceTestData
    {
        float StepIndex { get; }

        float CycleIndex { get; }

        float Current { get; }

        float Voltage { get; }

        float ChargeCapacity { get; }

        float DischargeCapacity { get; }
    }
}
