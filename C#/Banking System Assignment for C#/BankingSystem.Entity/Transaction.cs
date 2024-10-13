using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Entity
{
    public class Transaction
    {
        public Account account { get; set; }
        public string description { get; set; }
        public DateTime transactionDateTime { get; set; }
        public string transactionType { get; set; } 
        public float transactionAmount { get; set; }
    }
}
