using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codevil.TemplateRepository.Model.Entities;
using Codevil.TemplateRepository.Entities;
using Codevil.TemplateRepository.Data.Factories;
using Codevil.TemplateRepository.Model.Factories;

namespace Codevil.TemplateRepository.Model.Repositories
{
    public abstract class Repository<TRow, TEntity> : Codevil.TemplateRepository.Repositories.Repository<TRow, TEntity>
        where TRow : class
        where TEntity : DataEntity
    {
        public Repository()
            : base(new BankDataContextFactory(), new EntityFactory(), new RowFactory())
        {
        }
    }
}
