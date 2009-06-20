using Codevil.TemplateRepository.Data.Factories;
using Codevil.TemplateRepository.Entities;
using Codevil.TemplateRepository.Model.Factories;
using Template = Codevil.TemplateRepository.Repositories;

namespace Codevil.TemplateRepository.Model.Repositories
{
    public abstract class Repository<TRow, TEntity> : Template.Repository<TRow, TEntity>
        where TRow : class
        where TEntity : DataEntity
    {
        public Repository()
            : base(new BankDataContextFactory(), new EntityFactory(), new RowFactory())
        {
        }
    }
}
