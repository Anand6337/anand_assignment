using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer
{
    public class Transaction
    {
        public Account account;
        private string description;
        public DateTime transactionDateTime;
        private string transactionType;
        private float transactionAmount;

        public Transaction(Account account, string description, string transactionType, float transactionAmount)
        {
            this.account = account;
            this.description = description;
            this.transactionType = transactionType;
            this.transactionAmount = transactionAmount;
            this.transactionDateTime = DateTime.Now;
        }
    }
}
