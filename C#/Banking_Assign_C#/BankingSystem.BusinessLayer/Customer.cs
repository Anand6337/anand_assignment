using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BankingSystem.Entity;

namespace BankingSystem.BusinessLayer
{
    public class Customer
    {
        Entity.Customer _customer;
        // Private attributes (confidential)
        private int customerId;
        private string firstName;
        private string lastName;
        private string emailAddress;
        private string phoneNumber;
        private string address;

        // Default constructor
        public Customer() { }
        public Customer(int customerId, string firstName, string lastName, string emailAddress, string phoneNumber, string address)
        {
            this.customerId = customerId;
            this.firstName = firstName;
            this.lastName = lastName;
            //this.emailAddress = emailAddress;
            //this.phoneNumber = phoneNumber;
            this.address = address;
            SetEmail(emailAddress);
            SetPhoneNumber(phoneNumber);
        }
        // Method to validate email
        public void SetEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (Regex.IsMatch(email, emailPattern))
            {
                this.emailAddress = email;
            }
            else
            {
                throw new ArgumentException("Invalid email address.");
            }
        }

        // Method to validate phone number
        public void SetPhoneNumber(string phoneNumber)
        {
            string phonePattern = @"^\d{10}$";
            if (Regex.IsMatch(phoneNumber, phonePattern))
            {
                this.phoneNumber = phoneNumber;
            }
            else
            {
                throw new ArgumentException("Invalid phone number. Must be 10 digits.");
            }
        }

        // Print Customer Details
        public void PrintCustomerInfo()
        {
            Console.WriteLine($"Customer ID: {customerId}\nFirst Name: {firstName}\nLast Name: {lastName}\nEmail: {emailAddress}\nPhone: {phoneNumber}\nAddress: {address}");
        }
    }
}
