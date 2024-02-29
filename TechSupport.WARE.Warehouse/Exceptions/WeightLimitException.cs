using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class WeightLimitException : Exception
    {
        public WeightLimitException() : base(){ }
        public WeightLimitException(string message) : base(message) { }
        public WeightLimitException(string message, Exception innerException) : base(message, innerException) { }
    }
}
