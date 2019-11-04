using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DataResult: IDataResult
    {
        public bool Success { get; private set; }
        public object Result { get; private set; }

        public System.Exception Exception { get; private set; }

        public DataResult(System.Exception exception)
        {
            Success = false;
            Exception = exception;
        }

        public DataResult(object result)
        {
            Success = true;
            Result = result;
        }
    }
}
