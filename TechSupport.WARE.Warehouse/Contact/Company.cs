﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// Represents a company entity with basic information.
    /// </summary>
    public class Company : ICompany
    {
        private string companyName;
        private readonly int companyCode;
        private string address;
        private int postalCode;
        private string country;
        private Contact contactPerson;

        /// <summary>
        /// Initializes a new instance of the <see cref="Company"/> class with specified parameters.
        /// </summary>
        /// <param name="companyName">The name of the company.</param>
        /// <param name="companyCode">The code of the company.</param>
        /// <param name="address">The address of the company.</param>
        /// <param name="country">The country where the company is located.</param>
        /// <param name="postalCode">The postal code of the company.</param>
        /// <exception cref="FormatException">Thrown when company name or country contains digits.</exception>
        public Company(string companyName, int companyCode, string address, string country, int postalCode) 
        {
            if (companyName.Any(Char.IsDigit))
                throw new FormatException($"Company name: {companyName} is not allowed to contain an Integer...");
            if (country.Any(Char.IsDigit))
                throw new FormatException($"Country: {country} is not allowed to contain an Integer...");
            this.companyName = companyName;
            this.companyCode = companyCode;
            this.address = address;
            this.country = country;
            this.postalCode = postalCode;
        }

        public Company(string companyName, int companyCode, string address, string country, int postalCode, Contact contactPerson)
        {
            if (companyName.Any(Char.IsDigit))
                throw new FormatException($"Company name: {companyName} is not allowed to contain an Integer...");
            if (country.Any(Char.IsDigit))
                throw new FormatException($"Country: {country} is not allowed to contain an Integer...");
            this.companyName = companyName;
            this.companyCode = companyCode;
            this.address = address;
            this.country = country;
            this.postalCode = postalCode;
            this.contactPerson = contactPerson;
        }

        public string CompanyName
        {
            get => companyName;
            set
            {
                if (value.Any(Char.IsDigit))
                    throw new FormatException($"Company name: {value} is not allowed to contain an Integer...");
                companyName = value;
            }
        }

        public int CompanyCode => companyCode;

        public string Address
        {
            get => address;
            set => address = value;
        }

        public int PostalCode
        {
            get => postalCode;
            set => postalCode = value;
        }

        public string Country
        {
            get => country;
            set 
            {
                if (value.Any(Char.IsDigit))
                    throw new FormatException($"Country: {value} is not allowed to contain an Integer...");
                country = value; 
            }
        }

        public Contact ContactPerson 
        {
            get => contactPerson;
            set => contactPerson = value;
        }
    }
}
