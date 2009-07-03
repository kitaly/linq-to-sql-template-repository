using Codevil.TemplateRepository.Handlers;
using Codevil.TemplateRepository.Data.Factories;
using Codevil.TemplateRepository.Model.Entities;
using Codevil.TemplateRepository.Model.Repositories;
using Codevil.TemplateRepository.Test.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codevil.TemplateRepository.Test.Controllers
{
    [TestClass]
    public class TransactionTest : DatabaseDependentTest
    {
        [TestMethod]
        public void TransactionRollbackTest()
        {
            PeopleRepository peopleRepository = new PeopleRepository();
            AccountsRepository accountsRepository = new AccountsRepository();
            BankDataContextFactory contextFactory = new BankDataContextFactory();

            Person createdPerson = new Person();
            createdPerson.Name = "transaction test";
            createdPerson.Document = "ZGMFX20A";
            createdPerson.Email = "zaft@no.tameni";

            Account createdAccount = new Account();
            createdAccount.Number = 2354235;
            createdAccount.Agency = 34;

            Transaction transaction = contextFactory.CreateTransaction();

            peopleRepository.Save(createdPerson, transaction);

            createdAccount.OwnerId = createdPerson.Id;

            accountsRepository.Save(createdAccount, transaction);

            transaction.Rollback();

            Assert.IsNull(peopleRepository.FindSingle(p => p.Name == createdPerson.Name));
            Assert.IsNull(accountsRepository.FindSingle(a => a.Number == createdAccount.Number));
        }

        [TestMethod]
        public void TransactionCommitRollbackTest()
        {
            PeopleRepository peopleRepository = new PeopleRepository();
            AccountsRepository accountsRepository = new AccountsRepository();
            BankDataContextFactory contextFactory = new BankDataContextFactory();

            Person createdPerson = new Person();
            createdPerson.Name = "transaction test";
            createdPerson.Document = "ZGMFX20A";
            createdPerson.Email = "zaft@no.tameni";

            Account createdAccount = new Account();
            createdAccount.Number = 2354235;
            createdAccount.Agency = 34;

            Transaction transaction = contextFactory.CreateTransaction();

            peopleRepository.Save(createdPerson, transaction);
            createdAccount.OwnerId = createdPerson.Id;
            accountsRepository.Save(createdAccount, transaction);

            transaction.Commit();

            Assert.IsNotNull(peopleRepository.FindSingle(p => p.Name == createdPerson.Name));
            Assert.IsNotNull(accountsRepository.FindSingle(a => a.Number == createdAccount.Number));

            transaction = contextFactory.CreateTransaction();

            accountsRepository.Delete(createdAccount, transaction);
            peopleRepository.Delete(createdPerson, transaction);

            transaction.Rollback();

            Assert.IsNotNull(peopleRepository.FindSingle(p => p.Name == createdPerson.Name));
            Assert.IsNotNull(accountsRepository.FindSingle(a => a.Number == createdAccount.Number));

            transaction = contextFactory.CreateTransaction();

            accountsRepository.Delete(createdAccount, transaction);
            peopleRepository.Delete(createdPerson, transaction);

            transaction.Commit();

            Assert.IsNull(peopleRepository.FindSingle(p => p.Name == createdPerson.Name));
            Assert.IsNull(accountsRepository.FindSingle(a => a.Number == createdAccount.Number));
        }
    }
}
