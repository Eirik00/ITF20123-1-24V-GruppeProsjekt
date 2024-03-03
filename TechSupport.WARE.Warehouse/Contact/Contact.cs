using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse.Exceptions;

namespace TechSupport.WARE.Warehouse
{
    public class Contact : IContact
    {
        private readonly string firstName, surname;
        private string email, country, address;
        private int phoneNumber, postalCode;

        public Contact(string firstName, string surname, string email, string address, string country, int phoneNumber, int postalCode)
        {
            if (firstName.Any(Char.IsDigit))
                throw new IntInStringException($"First name: {firstName} is not allowed to contain an Integer...");
            if (surname.Any(Char.IsDigit))
                throw new IntInStringException($"Surname: {surname} is not allowed to contain an Integer...");
            //if (!email.Contains("@"))
            //    throw new InvalidEmailException($"Email: {email} does not contain the symbol @...");
            //if (country.Any(Char.IsDigit))
            //    throw new IntInStringException($"Country: {country} is not allowed to contain an Integer...");
            this.firstName = firstName;
            this.surname = surname;
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
            set 
            {
                if (!value.Contains("@"))
                    throw new InvalidEmailException($"Email: {value} does not contain the symbol @...");
                this.email = value; 
            }
        }

        public string Country
        {
            get => this.country;
            set
            {
                if (value.Any(Char.IsDigit))
                    throw new IntInStringException($"Country: {value} is not allowed to contain an Integer...");
                this.country = value;
            }
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
