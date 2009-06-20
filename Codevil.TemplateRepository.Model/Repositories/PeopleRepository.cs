using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codevil.TemplateRepository.Model.Entities;
using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Data.Factories;
using Codevil.TemplateRepository.Model.Factories;
using Codevil.TemplateRepository.Repositories;

namespace Codevil.TemplateRepository.Model.Repositories
{
    public class PeopleRepository : Repository<PERSON, Person, BankDataContext>
    {
        public PeopleRepository()
            : base(new BankDataContextFactory(), new EntityFactory(), new RowFactory())
        {
        }

        protected override PERSON FindEntity(Person entity, BankDataContext context)
        {
            return FindSingle(p => p.Id == entity.Id, context);
        }

        protected override void BeforeSave(Person entity, PERSON row)
        {
            row.Document = entity.Document;
            row.Email = entity.Email;
            row.Name = entity.Name;
        }

        protected override void AfterSave(PERSON row, Person entity)
        {
            entity.Id = row.Id;
        }
    }
}
