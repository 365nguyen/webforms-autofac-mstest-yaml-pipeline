using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBankAccountRepository
    {
        BankAccount AddBankAccount(BankAccount obj);
        BankAccount GetBankAccountById(int id);
        List<BankAccount> GetAll();
        void UpdateBankAccount(BankAccount obj);
        BankAccount DeleteBankAccount(BankAccount obj);
    }
}
