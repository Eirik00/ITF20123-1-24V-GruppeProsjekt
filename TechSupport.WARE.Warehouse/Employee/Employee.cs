using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse.Exceptions;

namespace TechSupport.WARE.Warehouse
{
    public class Employee : Contact
    {

        public Employee(int employeeID, int accessLevel, string firstName, string surname, string email, string address, string country, int phoneNumber, int postalCode) : base(firstName, surname, email, address, country, phoneNumber, postalCode)
        {
            if (employeeID < 0)
                throw new FormatException($"Eployee ID: {employeeID} cannot be a negative integer.");
            if (accessLevel < 0)
                throw new FormatException($"Access Level: {accessLevel} cannot be a negative integer.");
            this.employeeID = employeeID;
            this.accessLevel = accessLevel;
        }

        public int employeeID
        {
            get => this.employeeID;
            set
            {
                if (employeeID < 0)
                    throw new FormatException($"Eployee ID: {employeeID} cannot be a negative integer.");
                this.employeeID = value;
            }
        }
        public int accessLevel
        {
            get => this.accessLevel;
            set
            {
                if (accessLevel < 0)
                    throw new FormatException($"Access Level: {accessLevel} cannot be a negative integer.");
                this.accessLevel = value;
            }
        }
    }
}
