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
        public void CreateTest()
        {
            Person p = new Person();
            p.Name = "gandamu strike freedom";
            p.Document = "ZGMFX20A";
            p.Email = "zaft@no.tameni";

            PeopleRepository repo = new PeopleRepository();
            repo.Save(p);

            Assert.AreNotEqual(0, p.Id);
        }
    }
}
