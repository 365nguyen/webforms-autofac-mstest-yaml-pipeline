using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IBankAccountService
    {
        List<BankAccount> GetAll();
        BankAccount Debit(BankAccount bankAccount, decimal amount);
        BankAccount Credit(BankAccount bankAccount, decimal amount);
        void FreezeAccount(BankAccount bankAccount);
        void UnfreezeAccount(BankAccount bankAccount);
    }
}
