using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data.Linq;
using Codevil.TemplateRepository.Entities;
using Codevil.TemplateRepository.Factories;

namespace Codevil.TemplateRepository.Repositories
{
    public abstract class Repository<TRow, TEntity, TDataContext> : IRepository<TRow,TEntity,TDataContext>
        where TRow : class
        where TEntity : DataEntity
        where TDataContext : DataContext
    {
        public IDataContextFactory<TDataContext> DataContextFactory { get; set; }
        public IEntityFactory EntityFactory { get; set; }
        public IRowFactory<TDataContext> RowFactory { get; set; }

        public Repository(IDataContextFactory<TDataContext> dataContextFactory, IEntityFactory entityFactory, IRowFactory<TDataContext> rowFactory)
        {
            this.DataContextFactory = dataContextFactory;
            this.EntityFactory = entityFactory;
            this.RowFactory = rowFactory;
        }

        protected abstract void MapToRow(TEntity entity, TRow row);
        protected abstract void MapToEntity(TRow row, TEntity entity);
        protected abstract TRow FindEntity(TEntity entity, TDataContext context);

        protected virtual void Update(TRow row, TEntity entity, TDataContext context)
        {
            this.MapToRow(entity, row);
            
            context.SubmitChanges();

            this.MapToEntity(row, entity);
        }

        protected virtual void Create(TEntity entity, TDataContext context)
        {
            TRow row = (TRow)this.RowFactory.Create(typeof(TRow));
            Table<TRow> table = (Table<TRow>)this.RowFactory.CreateTable(typeof(TRow), context);

            this.MapToRow(entity, row);

            table.InsertOnSubmit(row);
            context.SubmitChanges();

            this.MapToEntity(row, entity);
        }

        public void Save(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", String.Format(CultureInfo.CurrentCulture, "Entity can't be null"));
            }

            TDataContext context = this.DataContextFactory.Create();

            try
            {
                TRow retrievedRow = this.FindEntity(entity, context);

                if (retrievedRow != null)
                {
                    this.Update(retrievedRow, entity, context);
                }
                else
                {
                    this.Create(entity, context);
                }
            }
            finally
            {
                context.Dispose();
            }
        }

        public void Save(TEntity entity, TDataContext context)
        {
            TRow retrievedRow = this.FindEntity(entity, context);

            if (retrievedRow != null)
            {
                this.Update(retrievedRow, entity, context);
            }
            else
            {
                this.Create(entity, context);
            }
        }

        public TEntity FindSingle(Func<TRow, bool> exp)
        {
            TDataContext context = this.DataContextFactory.Create();
            TEntity entity = null;

            try
            {
                TRow row = this.FindSingle(exp, context);

                if (row != null)
                {
                    entity = (TEntity)EntityFactory.Create(row);
                }
                else
                {
                    entity = null;
                }
            }
            finally
            {
                context.Dispose();
            }

            return entity;
        }

        public IList<TEntity> Find(Func<TRow, bool> exp)
        {
            TDataContext context = (TDataContext)this.DataContextFactory.Create();

            IList<TEntity> list = new List<TEntity>();

            try
            {
                list = this.ToEntity(this.Find(exp, context));
            }
            finally
            {
                context.Dispose();
            }

            return list;
        }

        protected TRow FindSingle(Func<TRow, bool> exp, TDataContext context)
        {
            IList<TRow> rows = this.Find(exp, context);

            if (rows.Count > 1)
            {
                throw new InvalidOperationException("Query matches more than one entry");
            }
            else if (rows.Count == 0)
            {
                return null;
            }
            else
            {
                return rows.Single();
            }
        }

        protected IList<TRow> Find(Func<TRow, bool> exp, TDataContext context)
        {
            return context.GetTable<TRow>().Where(exp).ToList();
        }

        protected IList<TEntity> ToEntity(IList<TRow> list)
        {
            IList<TEntity> entityList = new List<TEntity>();

            foreach (TRow item in list)
            {
                entityList.Add((TEntity)EntityFactory.Create(item));
            }

            return entityList;
        }
    }
}
