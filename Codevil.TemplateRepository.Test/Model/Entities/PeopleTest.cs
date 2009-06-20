using Codevil.TemplateRepository.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codevil.TemplateRepository.Test.Model.Entities
{
    [TestClass]
    public class PeopleTest
    {
        [TestMethod]
        public void EqualsTest()
        {
            Person person1 = new Person();
            person1.Document = "5555";
            person1.Email = "ansdklna@adnjka.com";
            person1.Name = "Eascn ASd";
            person1.Id = 4;

            Person person2 = new Person();
            person2.Document = "5555";
            person2.Email = "ansdklna@adnjka.com";
            person2.Name = "Eascn ASd";
            person2.Id = 4;

            Person person3 = new Person();
            person3.Document = "5555 ";
            person3.Email = "ansdklna@adnjka.com";
            person3.Name = "Eascn ASd";
            person3.Id = 4;

            Person person4 = new Person();
            person4.Document = "5555";
            person4.Email = "ansdklna@ adnjka.com";
            person4.Name = "Eascn ASd";
            person4.Id = 4;

            Person person5 = new Person();
            person5.Document = "5555";
            person5.Email = "ansdklna@adnjka.com";
            person5.Name = "Eascn ASd ";
            person5.Id = 4;

            Person person6 = new Person();
            person6.Document = "5555";
            person6.Email = "ansdklna@adnjka.com";
            person6.Name = "Eascn ASd";
            person6.Id = 3;

            Assert.AreEqual(person1, person1);
            Assert.AreEqual(person2, person1);
            Assert.AreNotEqual(person3, person1);
            Assert.AreNotEqual(person4, person1);
            Assert.AreNotEqual(person5, person1);
            Assert.AreNotEqual(person6, person1);
        }
    }
}
