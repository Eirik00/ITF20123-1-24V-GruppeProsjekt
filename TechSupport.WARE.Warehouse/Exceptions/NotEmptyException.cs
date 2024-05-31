using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{ 
    public class NotEmptyException : Exception
    {
        public NotEmptyException() : base() { }
        public NotEmptyException(string message) : base(message) { }
        public NotEmptyException(string message, Exception innerException) : base(message, innerException) { }
    }
}
