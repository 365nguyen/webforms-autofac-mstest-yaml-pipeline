using Autofac;
using Autofac.Core;
using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using Service;
using System;

namespace MSTest.Test
{
    [TestClass]
    public class BankAccountTest:BaseTest
    {

        [TestMethod]
        public void Credit_Should_UpdateBalanceCorrectly_DuringCreditAction()
        {
            // arrange
            decimal beginningBalance = 5.06M;
            decimal depositAmount = 3.01M;
            decimal expectedBalance = 8.07M;
            BankAccount bankAccountBefore = new BankAccount
            {
                AccountNumber = "1234",
                Balance = beginningBalance
            };
            BankAccount bankAccountAfter = new BankAccount
            {
                AccountNumber = "1234",
                Balance = expectedBalance
            };

            using (var mock = AutoMock.GetLoose())
            {
                //arrange
                mock.Mock<IBankAccountService>().Setup(x => x.Credit(bankAccountBefore,depositAmount)).Returns(bankAccountAfter);

                // act
                var cls = mock.Create<IBankAccountService>();
                var actual = cls.Credit(bankAccountBefore,depositAmount);

                // assert
                mock.Mock<IBankAccountService>().Verify(x => x.Credit(bankAccountBefore,depositAmount), Times.Exactly(1));
                Assert.AreEqual(bankAccountAfter.Balance, actual.Balance);
            }
        }

        [TestMethod]
        public void UpdateBalanceCorrectly_DuringDebitAction()
        {
            // arrange	
            decimal beginningBalance = 12.05M;
            decimal debitAmount = 2.02M;
            decimal expectedBalance = 10.03M;
            BankAccount bankAccountBefore = new BankAccount
            {
                AccountNumber = "1234",
                Balance = beginningBalance
            };
            BankAccount bankAccountAfter = new BankAccount
            {
                AccountNumber = "1234",
                Balance = expectedBalance
            };
            using (var mock = AutoMock.GetLoose())
            {
                //arrange
                mock.Mock<IBankAccountService>().Setup(x => x.Debit(bankAccountBefore, debitAmount)).Returns(bankAccountAfter);

                // act
                var cls = mock.Create<IBankAccountService>();
                var actual = cls.Debit(bankAccountBefore, debitAmount);

                // assert
                mock.Mock<IBankAccountService>().Verify(x => x.Debit(bankAccountBefore, debitAmount), Times.Exactly(1));
                Assert.AreEqual(bankAccountAfter.Balance, actual.Balance);
            }
        }

        [TestMethod]
        public void FreezeAccount_WhenNegativeBalanceBelowThreshold()
        {
            // arrange
            decimal beginningBalance = 1M;
            decimal debitAmount = Constants.AccountThresholds.FreezeBalance + 1;
            BankAccount bankAccountBefore = new BankAccount
            {
                AccountNumber = "1234",
                Balance = beginningBalance,
                Frozen = 0
            };
            BankAccount bankAccountAfterDebit = new BankAccount
            {
                AccountNumber = "1234",
                Frozen = 1
            };
            using (var mock = AutoMock.GetLoose())
            {
                //arrange
                mock.Mock<IBankAccountService>().Setup(x => x.Debit(bankAccountBefore, debitAmount)).Returns(bankAccountAfterDebit);

                // act
                var cls = mock.Create<IBankAccountService>();
                // act 1: withdraw money to negative balance
                var actual = cls.Debit(bankAccountBefore, debitAmount);
                // assert 1
                mock.Mock<IBankAccountService>().Verify(x => x.Debit(bankAccountBefore, debitAmount), Times.Exactly(1));
                Assert.IsTrue(bankAccountAfterDebit.Frozen == 1);
            }
        }

        [TestMethod]
        public void UnfreezeAccount_WhenPositiveBalanceReached()
        {
            // arrange
            decimal beginningBalance = 1;
            decimal debitAmount = Constants.AccountThresholds.FreezeBalance + 1;

            decimal creditAmount = Constants.AccountThresholds.FreezeBalance + 2;
            BankAccount bankAccountBefore = new BankAccount
            {
                AccountNumber = "1234",
                Balance = beginningBalance,
                Frozen = 0
            };
            BankAccount bankAccountAfterDebit = new BankAccount
            {
                AccountNumber = "1234",
                Frozen = 1
            };
            BankAccount bankAccountAfterCredit = new BankAccount
            {
                AccountNumber = "1234",
                Frozen = 0
            };
            using (var mock = AutoMock.GetLoose())
            {
                //arrange
                mock.Mock<IBankAccountService>().Setup(x => x.Debit(bankAccountBefore, debitAmount)).Returns(bankAccountAfterDebit);
                mock.Mock<IBankAccountService>().Setup(x => x.Credit(bankAccountAfterDebit, creditAmount)).Returns(bankAccountAfterCredit);

                // act
                var cls = mock.Create<IBankAccountService>();
                // act 1: withdraw money to negative balance
                var actual = cls.Debit(bankAccountBefore, debitAmount);
                // assert 1
                mock.Mock<IBankAccountService>().Verify(x => x.Debit(bankAccountBefore, debitAmount), Times.Exactly(1));
                Assert.IsTrue(bankAccountAfterDebit.Frozen == 1);
                // act 2: deposit money to positive balance
                actual = cls.Credit(bankAccountAfterDebit, creditAmount);
                // assert 2
                mock.Mock<IBankAccountService>().Verify(x => x.Credit(bankAccountAfterDebit, creditAmount), Times.Exactly(1));
                Assert.IsFalse(bankAccountAfterCredit.Frozen == 1);
            }
        }

        [TestMethod]
        public void Credit_Should_ThrowException_WhenNegativeCreditAmount()
        {
            // arrange
            decimal beginningBalance = 12.05M;
            decimal creditAmount = -5.0M;

            BankAccount bankAccountBefore = new BankAccount
            {
                AccountNumber = "1234",
                Balance = beginningBalance
            };

            // act and assert
            using (var mock = AutoMock.GetLoose())
            {
                //arrange
                mock.Mock<IBankAccountService>().Setup(x => x.Credit(bankAccountBefore, creditAmount)).Throws(new ArgumentOutOfRangeException());

                // act and assert
                var cls = mock.Create<IBankAccountService>();
                // assert
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => cls.Credit(bankAccountBefore, creditAmount));
            }
        }

        [TestMethod]
        public void ThrowException_WhenNegativeDebitAmount()
        {
            // arrange
            decimal beginningBalance = 12.05M;
            decimal debitAmount = -5.0M;

            BankAccount bankAccountBefore = new BankAccount
            {
                AccountNumber = "1234",
                Balance = beginningBalance
            };

            // act and assert
            using (var mock = AutoMock.GetLoose())
            {
                //arrange
                mock.Mock<IBankAccountService>().Setup(x => x.Debit(bankAccountBefore, debitAmount)).Throws(new ArgumentOutOfRangeException());

                // act and assert
                var cls = mock.Create<IBankAccountService>();
                // assert
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => cls.Debit(bankAccountBefore, debitAmount));
            }
        }
    }
}
