using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse.Delivery
{
    public class OutgoingException : Exception
    {
        public OutgoingException()
        {
        }

        public OutgoingException(string message) : base(message)
        {
        }

        public OutgoingException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class MissingPackageListException : OutgoingException
    {
        public MissingPackageListException(string message) : base(message)
        {
        }
    }

    public class MissingContactException : OutgoingException
    {
        public MissingContactException(string message) : base(message)
        {
        }
    }




















    public class MissingHourAndMinuteException : OutgoingException
    {
        public MissingHourAndMinuteException(string message) : base(message)
        {
        }
    }
}
