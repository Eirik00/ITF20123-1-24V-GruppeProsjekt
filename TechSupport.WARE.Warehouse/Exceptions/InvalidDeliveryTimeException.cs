using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.Exceptions
{
    public class InvalidDeliveryTimeException : Exception
    {
        public InvalidDeliveryTimeException() { }

        public InvalidDeliveryTimeException(string message) : base(message) { }

        public InvalidDeliveryTimeException(string message, Exception innerException) : base(message, innerException) { }
    }
}
