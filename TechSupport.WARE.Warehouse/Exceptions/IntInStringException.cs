using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.Exceptions
{
    public class IntInStringException : Exception
    {
        public IntInStringException() : base() { }
        public IntInStringException(string message) : base(message) { }
        public IntInStringException(string message, Exception innerException) : base(message, innerException) { }
    }
}
