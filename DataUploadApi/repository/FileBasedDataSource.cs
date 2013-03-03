using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi
{
    interface FileBasedDataSource<T, R>
    {
        T getTestResults();

    }
}
