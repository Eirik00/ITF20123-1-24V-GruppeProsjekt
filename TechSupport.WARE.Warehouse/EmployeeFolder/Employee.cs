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
        private int _employeeId;
        private int _accessLevel;

        public Employee(int employeeID, int accessLevel, string firstName, string surname, string email, string address, string country, int phoneNumber, int postalCode) : base(firstName, surname, email, address, country, phoneNumber, postalCode)
        {
            if (employeeID < 0)
                throw new FormatException($"Eployee ID: {employeeID} cannot be a negative integer.");
            if (accessLevel < 0)
                throw new FormatException($"Access Level: {accessLevel} cannot be a negative integer.");
            this._employeeId = employeeID;
            this._accessLevel = accessLevel;
        }

        public int EmployeeID
        {
            get => this._employeeId;
            set
            {
                if (_employeeId < 0)
                    throw new FormatException($"Eployee ID: {_employeeId} cannot be a negative integer.");
                this._employeeId = value;
            }
        }
        public int AccessLevel
        {
            get => this._accessLevel;
            set
            {
                if (_accessLevel < 0)
                    throw new FormatException($"Access Level: {_accessLevel} cannot be a negative integer.");
                this._accessLevel = value;
            }
        }
    }
}
