using System.Data.Linq;
using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Model.Entities;

namespace Codevil.TemplateRepository.Model.Repositories
{
    public class PeopleRepository : Repository<PERSON, Person>
    {
        protected override PERSON FindEntity(Person entity, DataContext context)
        {
            return FindSingle(p => p.Id == entity.Id, context);
        }

        protected override void BeforeSave(PERSON row, Person entity)
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
