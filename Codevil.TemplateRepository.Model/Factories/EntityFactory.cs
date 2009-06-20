using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codevil.TemplateRepository.Model.Entities;
using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Entities;
using Codevil.TemplateRepository.Factories;

namespace Codevil.TemplateRepository.Model.Factories
{
    public class EntityFactory : IEntityFactory
    {
        public DataEntity Create(object row)
        {
            Type rowType = row.GetType();

            switch (rowType.Name)
            {
                case "ACCOUNT":
                    return new Account((ACCOUNT)row);
                case "PERSON":
                    return new Person((PERSON)row);
                default:
                    throw new ArgumentException("Invalid type: " + rowType.Name, "row");
            }
        }
    }
}
