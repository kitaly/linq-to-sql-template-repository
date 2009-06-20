using System;
using System.Collections.Generic;
using Codevil.TemplateRepository.Entities;
using Codevil.TemplateRepository.Factories;
namespace Codevil.TemplateRepository.Repositories
{
    public interface IRepository<TRow, TEntity, TDataContext>
        where TRow : class
        where TEntity : DataEntity
        where TDataContext : System.Data.Linq.DataContext
    {
        IDataContextFactory<TDataContext> DataContextFactory { get; set; }
        IList<TEntity> Find(Func<TRow, bool> exp);
        TEntity FindSingle(Func<TRow, bool> exp);
        void Save(TEntity entity);
    }
}
