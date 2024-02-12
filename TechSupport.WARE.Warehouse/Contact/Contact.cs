using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WARE.Warehouse
{
    public class Contact : IContact
    {
        private string firstName, surname, email, country, address;
        private int phonesNumber, postalCode;

        public Contact(string firstName, string surName, string email, string country, string address, int phoneNumber, int postalCode)
        {
            this.firstName = firstName;
            this.surname = surName;
            this.email = email;
            this.country = country;
            this.address = address;
            this.phonesNumber = phoneNumber;
            this.postalCode = postalCode;
        }

        public string FirstName => this.firstName;

        public string Surname => this.surname;

        public string Email => this.email;

        public string Country => this.Country;

        public int PhoneNumber => this.phonesNumber;

        public string Address => this.address;

        public int PostalCode => this.postalCode;

    }
}
