using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class Company
    {
        private readonly String companyName;
        private readonly int companyCode;
        private readonly String address;
        private readonly int postalCode;
        private readonly String country;
        private Contact contactPerson;

        public Company(String companyName, int companyCode, String address, String country, int postalCode) 
        { 
            this.companyName = companyName;
            this.companyCode = companyCode;
            this.address = address;
            this.country = country;
            this.postalCode = postalCode;
            this.contactPerson = new Contact("", "", "", "", "", 0, 0);
        }

        public Contact ContactPerson 
        {
            get { return this.contactPerson; }
            set { this.contactPerson = value; }
        }
    }
}
