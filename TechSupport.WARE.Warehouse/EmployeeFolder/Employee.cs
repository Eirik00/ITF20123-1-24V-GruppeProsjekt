using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.WARE.Warehouse.Exceptions;

namespace TechSupport.WARE.Warehouse
{
    /// <summary>
    /// Represents an employee with access to the warehouse system.
    /// </summary>
    public class Employee : Contact
    {
        private int _employeeId;
        private int _accessLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="employeeID">The unique identifier for the employee.</param>
        /// <param name="accessLevel">The access level of the employee.</param>
        /// <param name="firstName">The first name of the employee.</param>
        /// <param name="surname">The surname of the employee.</param>
        /// <param name="email">The email address of the employee.</param>
        /// <param name="address">The address of the employee.</param>
        /// <param name="country">The country of the employee.</param>
        /// <param name="phoneNumber">The phone number of the employee.</param>
        /// <param name="postalCode">The postal code of the employee.</param>
        public Employee(int employeeID, int accessLevel, string firstName, string surname, string email, string address, string country, int phoneNumber, int postalCode) : base(firstName, surname, email, address, country, phoneNumber, postalCode)
        {
            if (employeeID < 0)
                throw new FormatException($"Eployee ID: {employeeID} cannot be a negative integer.");
            if (accessLevel < 0)
                throw new FormatException($"Access Level: {accessLevel} cannot be a negative integer.");
            this._employeeId = employeeID;
            this._accessLevel = accessLevel;
        }

        /// <summary>
        /// Gets or sets the employee ID.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the access level of the employee.
        /// </summary>
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
