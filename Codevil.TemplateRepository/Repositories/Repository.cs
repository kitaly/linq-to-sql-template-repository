using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data.Linq;
using Codevil.TemplateRepository.Entities;
using Codevil.TemplateRepository.Factories;
using System.Transactions;

namespace Codevil.TemplateRepository.Repositories
{
    public abstract class Repository<TRow, TEntity> : IRepository<TRow, TEntity>
        where TRow : class
        where TEntity : DataEntity
    {
        public IDataContextFactory DataContextFactory { get; set; }
        public IEntityFactory EntityFactory { get; set; }
        public IRowFactory RowFactory { get; set; }

        public Repository(IDataContextFactory dataContextFactory, IEntityFactory entityFactory, IRowFactory rowFactory)
            : this(dataContextFactory, entityFactory)
        {
            this.RowFactory = rowFactory;
        }

        public Repository(IDataContextFactory dataContextFactory, IEntityFactory entityFactory)
        {
            this.DataContextFactory = dataContextFactory;
            this.EntityFactory = entityFactory;
            this.RowFactory = new RowFactory();
        }

        protected abstract void BeforeSave(TEntity entity, TRow row);
        protected abstract void AfterSave(TRow row, TEntity entity);
        protected abstract TRow FindEntity(TEntity entity, DataContext context);

        protected virtual void Update(TRow row, TEntity entity, DataContext context)
        {
            this.BeforeSave(entity, row);

            context.SubmitChanges();

            this.AfterSave(row, entity);
        }

        protected virtual void Create(TEntity entity, DataContext context)
        {
            TRow row = (TRow)this.RowFactory.Create(typeof(TRow));
            Table<TRow> table = (Table<TRow>)this.RowFactory.CreateTable(typeof(TRow), context);

            this.BeforeSave(entity, row);

            table.InsertOnSubmit(row);
            context.SubmitChanges();

            this.AfterSave(row, entity);
        }

        public void Save(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", String.Format(CultureInfo.CurrentCulture, "Entity can't be null"));
            }

            DataContext context = this.DataContextFactory.Create();

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

        public void Save(TEntity entity, UnitOfWork unitOfWork)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", String.Format(CultureInfo.CurrentCulture, "Entity can't be null"));
            }

            DataContext context = unitOfWork.DataContext;

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
            catch
            {
                unitOfWork.Rollback();

                throw;
            }
        }

        public void Save(TEntity entity, DataContext context)
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
            DataContext context = this.DataContextFactory.Create();
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
            DataContext context = (DataContext)this.DataContextFactory.Create();

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

        protected TRow FindSingle(Func<TRow, bool> exp, DataContext context)
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

        protected IList<TRow> Find(Func<TRow, bool> exp, DataContext context)
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
