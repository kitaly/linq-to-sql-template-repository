using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codevil.TemplateRepository.Model.Entities;
using Codevil.TemplateRepository.Model.Repositories;

namespace Codevil.TemplateRepository.Test.Repositories
{
    [TestClass]
    public class PeopleRepositoryTest
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
    }
}
