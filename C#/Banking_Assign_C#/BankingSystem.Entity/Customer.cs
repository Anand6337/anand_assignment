using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.BusinessLayer;

namespace BankingSystem.Entity
{
    public class Customer
    {
        public int CustomerId { get; set; }

        // Getter and Setter for First Name
        public string FirstName { get; set; }

        // Getter and Setter for Last Name
        public string LastName { get; set; }

        // Getter and Setter for Email Address
        public string EmailAddress { get; set; }

        // Getter and Setter for Phone Number
        public string PhoneNumber { get; set; }

        // Getter and Setter for Address
        public string Address{ get; set; }
    }

}
