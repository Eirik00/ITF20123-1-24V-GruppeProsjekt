using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse.Exceptions;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// Represents a contact entity with basic information.
    /// </summary>
    public class Contact : IContact
    {
        private readonly string firstName, surname;
        private string email, country, address;
        private int phoneNumber, postalCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class with specified parameters.
        /// </summary>
        /// <param name="firstName">The first name of the contact.</param>
        /// <param name="surname">The surname of the contact.</param>
        /// <param name="email">The email address of the contact.</param>
        /// <param name="address">The address of the contact.</param>
        /// <param name="country">The country where the contact is located.</param>
        /// <param name="phoneNumber">The phone number of the contact.</param>
        /// <param name="postalCode">The postal code of the contact.</param>
        /// <exception cref="FormatException">Thrown when first name or surname contains digits, or country contains digits.</exception>
        /// <exception cref="InvalidEmailException">Thrown when the email address does not contain the symbol '@'.</exception>
        public Contact(string firstName, string surname, string email, string address, string country, int phoneNumber, int postalCode)
        {
            if (firstName.Any(Char.IsDigit))
                throw new FormatException($"First name: {firstName} is not allowed to contain an Integer...");
            if (surname.Any(Char.IsDigit))
                throw new FormatException($"Surname: {surname} is not allowed to contain an Integer...");
            if (!email.Contains("@"))
                throw new InvalidEmailException($"Email: {email} does not contain the symbol @...");
            if (country.Any(Char.IsDigit))
                throw new FormatException($"Country: {country} is not allowed to contain an Integer...");
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
                    throw new FormatException($"Country: {value} is not allowed to contain an Integer...");
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
