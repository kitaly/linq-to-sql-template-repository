using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codevil.TemplateRepository.Model.Entities;
using Codevil.TemplateRepository.Model.Repositories;
using System.Transactions;
using Codevil.TemplateRepository.Test.Data;
using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Entities;
using Codevil.TemplateRepository.Data.Factories;

namespace Codevil.TemplateRepository.Test.Model.Repositories
{
    [TestClass]
    public class PeopleRepositoryTest : DatabaseDependentTest
    {
        [TestMethod]
        public void CreateFindUpdateTest()
        {
            Person createdPerson = new Person();
            createdPerson.Name = "gandamu strike freedom";
            createdPerson.Document = "ZGMFX20A";
            createdPerson.Email = "zaft@no.tameni";

            PeopleRepository repository = new PeopleRepository();

            repository.Save(createdPerson);

            Assert.AreNotEqual(0, createdPerson.Id);

            Person retrievedPerson = repository.FindSingle(p => p.Id == createdPerson.Id);

            Assert.AreEqual(createdPerson, retrievedPerson);

            retrievedPerson.Email = "kira.yamato@freedom.jp";

            repository.Save(retrievedPerson);

            retrievedPerson = repository.FindSingle(p => p.Id == createdPerson.Id);

            Assert.AreEqual("kira.yamato@freedom.jp", retrievedPerson.Email);
        }

        [TestMethod]
        public void TransactionTest()
        {
            Person createdPerson = new Person();
            createdPerson.Name = "transaction test";
            createdPerson.Document = "ZGMFX20A";
            createdPerson.Email = "zaft@no.tameni";

            PeopleRepository repository = new PeopleRepository();

            BankDataContextFactory contextFactory = new BankDataContextFactory();

            UnitOfWork unitOfWork = contextFactory.CreateUnitOfWork();

            repository.Save(createdPerson, unitOfWork);

            unitOfWork.Rollback();

            Assert.IsNull(repository.FindSingle(p => p.Name == createdPerson.Name));

            unitOfWork = contextFactory.CreateUnitOfWork();

            repository.Save(createdPerson, unitOfWork);

            unitOfWork.Commit();

            Assert.IsNotNull(repository.FindSingle(p => p.Name == createdPerson.Name));
        }
    }
}
