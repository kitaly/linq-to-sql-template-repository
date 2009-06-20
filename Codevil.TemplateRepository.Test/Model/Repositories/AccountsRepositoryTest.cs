using Codevil.TemplateRepository.Model.Entities;
using Codevil.TemplateRepository.Model.Repositories;
using Codevil.TemplateRepository.Test.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codevil.TemplateRepository.Test.Model.Repositories
{
    [TestClass]
    public class AccountsRepositoryTest : DatabaseDependentTest
    {
        [TestMethod]
        public void CreateFindUpdateTest()
        {
            Person createdPerson = new Person();
            createdPerson.Name = "gandamu strike freedom";
            createdPerson.Document = "ZGMFX20A";
            createdPerson.Email = "zaft@no.tameni";

            PeopleRepository peopleRepository = new PeopleRepository();

            peopleRepository.Save(createdPerson);

            Account createdAccount = new Account();
            createdAccount.Agency = 435;
            createdAccount.Number = 123123123;
            createdAccount.OwnerId = createdPerson.Id;

            AccountsRepository repository = new AccountsRepository();

            repository.Save(createdAccount);

            Assert.AreNotEqual(0, createdAccount.Id);

            Account retrievedAccount = repository.FindSingle(a => a.Id == createdAccount.Id);

            Assert.AreEqual(createdAccount, retrievedAccount);

            retrievedAccount.Agency = 666;

            repository.Save(retrievedAccount);

            retrievedAccount = repository.FindSingle(a => a.Id == createdAccount.Id);

            Assert.AreEqual(666, retrievedAccount.Agency);
        }

        [TestMethod]
        public void FindByPersonNameTest()
        {
            Person createdPerson = new Person();
            createdPerson.Name = "person name test";
            createdPerson.Document = "ZGMFX20A";
            createdPerson.Email = "zaft@no.tameni";

            PeopleRepository peopleRepository = new PeopleRepository();

            peopleRepository.Save(createdPerson);

            Account createdAccount = new Account();
            createdAccount.Agency = 435;
            createdAccount.Number = 123123123;
            createdAccount.OwnerId = createdPerson.Id;

            AccountsRepository repository = new AccountsRepository();

            repository.Save(createdAccount);

            Assert.AreNotEqual(0, createdAccount.Id);

            Account retrievedAccount = repository.FindSingle(a => a.PERSON.Name == createdPerson.Name);

            Assert.AreEqual(createdAccount, retrievedAccount);
        }
    }
}
