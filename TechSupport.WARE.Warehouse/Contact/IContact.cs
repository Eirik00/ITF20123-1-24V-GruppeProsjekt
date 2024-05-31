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
        /// No Set available
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// Get the surname of Contact.
        /// No Set avaialable
        /// </summary>
        string Surname { get; }

        /// <summary>
        /// Get or Set the Contact's email.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Get or Set the Contact's country.
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// Get or Set the phone number of the Contact.
        /// </summary>
        int PhoneNumber { get; set; }

        /// <summary>
        /// Get or Set the Contact's address.
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// Get or Set the Contact's postal code.
        /// </summary>
        int PostalCode { get; set; }
    }
}
