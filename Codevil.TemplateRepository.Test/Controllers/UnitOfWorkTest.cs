using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codevil.TemplateRepository.Test.Data;
using Codevil.TemplateRepository.Controllers;
using Codevil.TemplateRepository.Data.Factories;
using Codevil.TemplateRepository.Model.Repositories;
using Codevil.TemplateRepository.Model.Entities;

namespace Codevil.TemplateRepository.Test.Controllers
{
    [TestClass]
    public class UnitOfWorkTest : DatabaseDependentTest
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

            UnitOfWork unitOfWork = contextFactory.CreateUnitOfWork();

            peopleRepository.Save(createdPerson, unitOfWork);

            createdAccount.OwnerId = createdPerson.Id;

            accountsRepository.Save(createdAccount, unitOfWork);

            unitOfWork.Rollback();

            Assert.IsNull(peopleRepository.FindSingle(p => p.Name == createdPerson.Name));
            Assert.IsNull(accountsRepository.FindSingle(a => a.Number == createdAccount.Number));
        }

        [TestMethod]
        public void TransactionCommitTest()
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

            UnitOfWork unitOfWork = contextFactory.CreateUnitOfWork();

            peopleRepository.Save(createdPerson, unitOfWork);

            createdAccount.OwnerId = createdPerson.Id;

            accountsRepository.Save(createdAccount, unitOfWork);

            unitOfWork.Commit();

            Assert.IsNotNull(peopleRepository.FindSingle(p => p.Name == createdPerson.Name));
            Assert.IsNotNull(accountsRepository.FindSingle(a => a.Number == createdAccount.Number));
        }
    }
}
