using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class Company : ICompany
    {
        private string companyName;
        private readonly int companyCode;
        private string address;
        private int postalCode;
        private string country;
        private Contact contactPerson;

        public Company(string companyName, int companyCode, string address, string country, int postalCode) 
        { 
            this.companyName = companyName;
            this.companyCode = companyCode;
            this.address = address;
            this.country = country;
            this.postalCode = postalCode;
            this.contactPerson = new Contact("", "", "", "", "", 0, 0);
        }

        public Company(string companyName, int companyCode, string address, string country, int postalCode, Contact contactPerson)
        {
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
            set => companyName = value;
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
            set => country = value;
        }

        public Contact ContactPerson 
        {
            get => contactPerson;
            set => contactPerson = value;
        }
    }
}
