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
        private readonly string firstName, surname;
        private string email, country, address;
        private int phoneNumber, postalCode;

        public Contact(string firstName, string surName, string email, string address, string country, int phoneNumber, int postalCode)
        {
            this.firstName = firstName;
            this.surname = surName;
            this.email = email;
            this.country = country;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.postalCode = postalCode;
        }

        public string FirstName => this.firstName;

        public string Surname => this.surname;

        public string Email
        {
            get => this.email;
            set => this.email = value;
        }

        public string Country
        {
            get => this.country;
            set => this.country = value;
        }

        public int PhoneNumber
        {
            get => this.phoneNumber;
            set => this.phoneNumber = value;
        }

        public string Address
        {
            get => this.address;
            set => this.address = value;
        }

        public int PostalCode
        {
            get => this.postalCode;
            set => this.postalCode = value;
        }

    }
}
