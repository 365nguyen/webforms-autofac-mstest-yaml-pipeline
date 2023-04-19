using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BankAccountService : IBankAccountService
    {
        public IBankAccountRepository BankAccountRepository { get; set; }
        public BankAccountService(IBankAccountRepository repository)
        {
            BankAccountRepository = repository;
        }

        public BankAccount Debit(BankAccount bankAccount, decimal amount)
        {
            if (amount > bankAccount.Balance)
            {
                // we allow negative balances under some conditions, we charge a service fee to customer
                // if balance is below threshold, freeze the account
                if (bankAccount.Balance < Constants.AccountThresholds.FreezeBalance)
                {
                    FreezeAccount(bankAccount);
                }
            }
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }
            bankAccount.Balance -= amount;
            BankAccountRepository.UpdateBankAccount(bankAccount);
            return bankAccount;
        }

        public BankAccount Credit(BankAccount bankAccount, decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }
            bankAccount.Balance += amount;
            if (bankAccount.Balance > Constants.AccountThresholds.FreezeBalance)
            {
                UnfreezeAccount(bankAccount);
            }
            BankAccountRepository.UpdateBankAccount(bankAccount);
            return bankAccount;
        }

        public void FreezeAccount(BankAccount bankAccount)
        {
            bankAccount.Frozen = 1;
            BankAccountRepository.UpdateBankAccount(bankAccount);
        }

        public void UnfreezeAccount(BankAccount bankAccount)
        {
            bankAccount.Frozen = 0;
            BankAccountRepository.UpdateBankAccount(bankAccount);
        }

        public List<BankAccount> GetAll()
        {
            return BankAccountRepository.GetAll();
        }
    }
}
