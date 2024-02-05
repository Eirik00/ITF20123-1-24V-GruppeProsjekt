using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class Company
    {
        String companyName;
        String companyCode;
        String address;
        String city;
        int postalCode;
        String country;
        Contact contactPerson;

        public Company(String companyName, String companyCode, String address, String city, int postalCode, String country) 
        { 
            this.companyName = companyName;
            this.companyCode = companyCode;
            this.address = address;
            this.city = city;
            this.postalCode = postalCode;
            this.country = country;
            this.contactPerson = new Contact("", "", "", "", "", 0, 0);
        }

        Contact ContactPerson 
        {
            get { return this.contactPerson; }
            set { this.contactPerson = value; }
        }
    }
}
