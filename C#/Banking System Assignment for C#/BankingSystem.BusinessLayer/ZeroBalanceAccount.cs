using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer
{
    public class ZeroBalanceAccount : Account
    {
        public ZeroBalanceAccount(Customer customer)
        : base("ZeroBalance", 0, customer) { }
        public override string ToString()
        {
            return $"ZeroBalanceAccount: {base.ToString()}";
        }
    }
}
