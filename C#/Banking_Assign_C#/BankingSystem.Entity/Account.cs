using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Entity
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public string AccountType { get; set; }
        public float AccountBalance { get; set; }
        public Customer Customer { get; set; }
    }
}
