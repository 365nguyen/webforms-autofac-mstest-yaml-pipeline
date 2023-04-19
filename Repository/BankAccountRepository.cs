using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BankAccountRepository : IBankAccountRepository
    {
        public Entities Entities { get; set; }

        public BankAccountRepository(Entities entities)
        {
            Entities = entities;
        }

        public BankAccount AddBankAccount(BankAccount obj)
        {
            BankAccount BankAccount = Entities.BankAccounts.Add(obj); ;
            Entities.SaveChanges();
            return BankAccount;
        }

        public BankAccount GetBankAccountById(int id)
        {
            return Entities.BankAccounts.Find(id);
        }

        public List<BankAccount> GetAll()
        {
            return Entities.BankAccounts.OrderBy(x => x.AccountNumber).ToList();
        }

        public void UpdateBankAccount(BankAccount obj)
        {
            Entities.Entry(obj).State = EntityState.Modified;
            Entities.SaveChanges();
        }

        public BankAccount DeleteBankAccount(BankAccount obj)
        {
            BankAccount BankAccount = Entities.BankAccounts.Remove(obj);
            Entities.SaveChanges();
            return BankAccount;
        }
    }
}
