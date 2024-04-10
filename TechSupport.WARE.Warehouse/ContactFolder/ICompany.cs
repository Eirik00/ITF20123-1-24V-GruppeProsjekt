using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    internal interface ICompany
    {
        /// <summary>
        /// Get or Set the name of the Company.
        /// </summary>
        string CompanyName { get; set; }

        /// <summary>
        /// Get the company code of the Company
        /// No setter available.
        /// </summary>
        int CompanyCode { get; }

        /// <summary>
        /// Get or Set the address of where the company is located.
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// Get or Set the PostalCode for the Company.
        /// </summary>
        int PostalCode { get; set; }

        /// <summary>
        /// Get or Set the country where the Company is located.
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// Get the Contact object, allows you to use getters for Contact
        /// Example: .Firstname
        /// </summary>
        Contact ContactPerson { get; set; }
    }
}
