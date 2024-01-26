using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    internal interface IContact
    {
        string FirstName { get; }
        string Surname { get; }
        string Email { get; }
        string Country { get; }
        int PhoneNumber { get; }
        string Address { get; }
        int PostalCode { get; }
    }
}
