using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.Exceptions
{
    public class InvalidContactCompanyInfoException : Exception
    {
        public InvalidContactCompanyInfoException() { }
        public InvalidContactCompanyInfoException(string message) : base(message) { }
        public InvalidContactCompanyInfoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
