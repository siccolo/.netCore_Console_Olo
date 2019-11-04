using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface IDataResult
    {
        bool Success { get; }
        object Result { get;}

        System.Exception Exception { get; }
    }
}
