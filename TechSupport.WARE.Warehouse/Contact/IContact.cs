using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    internal interface IContact
    {
        /// <summary>
        /// Get the first name of Contact.
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// Get the surname of Contact.
        /// </summary>
        string Surname { get; }

        /// <summary>
        /// Get the Contact's email.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Get the Contact's country.
        /// </summary>
        string Country { get; }

        /// <summary>
        /// Get the phone number of the Contact.
        /// </summary>
        int PhoneNumber { get; }

        /// <summary>
        /// Get the Contact's address.
        /// </summary>
        string Address { get; }

        /// <summary>
        /// Get the Contact's postal code.
        /// </summary>
        int PostalCode { get; }
    }
}
