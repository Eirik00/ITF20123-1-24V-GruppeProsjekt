using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.Exceptions
{
    public class InvalidPackageListException : Exception
    {
        public InvalidPackageListException() { }
        public InvalidPackageListException(string message) : base(message) { }
        public InvalidPackageListException(string message, Exception innerException) : base(message, innerException) { }
    }
}
