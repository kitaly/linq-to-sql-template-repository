using System;
using System.Collections.Generic;
using Codevil.TemplateRepository.Entities;
using Codevil.TemplateRepository.Factories;

namespace Codevil.TemplateRepository.Repositories
{
    public interface IRepository<TRow, TEntity>
        where TRow : class
        where TEntity : DataEntity
    {
        IDataContextFactory DataContextFactory { get; set; }
        IEntityFactory EntityFactory { get; set; }
        IRowFactory RowFactory { get; set; }
        bool AutoRollbackOnError { get; set; }

        IList<TEntity> Find(Func<TRow, bool> exp);
        TEntity FindSingle(Func<TRow, bool> exp);
        void Save(TEntity entity);
    }
}
