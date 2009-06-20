using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codevil.TemplateRepository.Model.Entities;

namespace Codevil.TemplateRepository.Test.Entities
{
    [TestClass]
    public class AccountsTest
    {
        [TestMethod]
        public void EqualsTest()
        {
            Account account1 = new Account();
            account1.Number = 452356;
            account1.Agency = 567;
            account1.OwnerId = 123;
            account1.Id = 4;

            Account account2 = new Account();
            account2.Number = 452356;
            account2.Agency = 567;
            account2.OwnerId = 123;
            account2.Id = 4;

            Account account3 = new Account();
            account3.Number = 4252356;
            account3.Agency = 567;
            account3.OwnerId = 123;
            account3.Id = 4;

            Account account4 = new Account();
            account4.Number = 452356;
            account4.Agency = 5627;
            account4.OwnerId = 123;
            account4.Id = 4;

            Account account5 = new Account();
            account5.Number = 452356;
            account5.Agency = 567;
            account5.OwnerId = 1223;
            account5.Id = 4;

            Account account6 = new Account();
            account6.Number = 452356;
            account6.Agency = 567;
            account6.OwnerId = 123;
            account6.Id = 42;

            Assert.AreEqual(account1, account1);
            Assert.AreEqual(account2, account1);
            Assert.AreNotEqual(account3, account1);
            Assert.AreNotEqual(account4, account1);
            Assert.AreNotEqual(account5, account1);
            Assert.AreNotEqual(account6, account1);
        }
    }
}
