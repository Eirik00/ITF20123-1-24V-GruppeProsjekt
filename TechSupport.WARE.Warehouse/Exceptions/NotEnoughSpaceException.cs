using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class NotEnoughSpaceException : Exception
    {
        public NotEnoughSpaceException() :base() {}
        public NotEnoughSpaceException(string message) : base(message) { }
        public NotEnoughSpaceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
