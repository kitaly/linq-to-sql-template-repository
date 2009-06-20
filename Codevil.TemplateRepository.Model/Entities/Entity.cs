using Codevil.TemplateRepository.Entities;
using Codevil.TemplateRepository.Factories;
using Codevil.TemplateRepository.Data.Factories;

namespace Codevil.TemplateRepository.Model.Entities
{
    public class Entity : DataEntity
    {
        public int Id { get; set; }
        public IDataContextFactory DataContextFactory { get; set; }

        public Entity()
        {
            this.DataContextFactory = new BankDataContextFactory();
        }
    }
}
