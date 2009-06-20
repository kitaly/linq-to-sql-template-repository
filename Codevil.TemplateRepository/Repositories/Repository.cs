using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using Codevil.TemplateRepository.Controllers;
using Codevil.TemplateRepository.Entities;
using Codevil.TemplateRepository.Factories;

namespace Codevil.TemplateRepository.Repositories
{
    /// <summary>
    /// <para>
    /// This is the base implementation of the IRepository interface for a generic type of Row and Entity
    /// </para>
    /// <para>
    /// It must be inherited and customized to match the specific needs of each type of entities in the model
    /// </para>
    /// </summary>
    /// <typeparam name="TRow"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class Repository<TRow, TEntity> : IRepository<TRow, TEntity>
        where TRow : class
        where TEntity : DataEntity
    {
        public IDataContextFactory DataContextFactory { get; set; }
        public IEntityFactory EntityFactory { get; set; }
        public IRowFactory RowFactory { get; set; }
        public bool AutoRollbackOnError { get; set; }

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
            this.AutoRollbackOnError = true;
        }

        #region save
        /// <summary>
        /// <remarks>
        /// This method must be overriden
        /// </remarks>
        /// <para>
        /// This method should setup the data
        /// from the entity to the row that is going to be inserted or updated
        /// </para>
        /// <para>
        /// This method is called on the very beginning of every save (create/update)
        /// operation
        /// </para>
        /// </summary>
        /// <param name="row">The row that will be inserted</param>
        /// <param name="entity">The entity that holds the information</param>
        protected abstract void BeforeSave(TRow row, TEntity entity);

        /// <summary>
        /// <para>
        /// This method will persist (create or update) an entity on the database
        /// using an auto-commit transaction
        /// </para>
        /// </summary>
        /// <param name="entity">The entity that is going to be persisted</param>
        public virtual void Save(TEntity entity)
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

        /// <summary>
        /// <para>
        /// This method will persist (create or update) an entity on the database
        /// using an unit of work to handle the transaction
        /// </para>
        /// <para>
        /// You can call it multiple times, for different repositories and entities
        /// and as long as you pass the same instance of a unit of work as a parameter,
        /// all operations will be enclosed in the same transaction. After that, you
        /// can decide to commit or rollback the transaction
        /// </para>
        /// </summary>
        /// <param name="entity">The entity that is going to be persisted</param>
        /// <param name="unitOfWork">The unit of work in which the operation will take place</param>
        public virtual void Save(TEntity entity, UnitOfWork unitOfWork)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", String.Format(CultureInfo.CurrentCulture, "Entity can't be null"));
            }

            DataContext context = unitOfWork.DataContext;

            try
            {
                this.Save(entity, context);
            }
            catch
            {
                if (this.AutoRollbackOnError)
                {
                    unitOfWork.Rollback();
                }

                throw;
            }
        }

        /// <summary>
        /// <para>
        /// This method will persist (create or update) an entity on the database
        /// using a specific context. This way, you can manually setup a context
        /// for using a specific kind of transaction and/or isolation level
        /// </para>
        /// <param name="entity">The entity that is going to be persisted</param>
        /// <param name="context">The context in which the operation will take place</param>
        public virtual void Save(TEntity entity, DataContext context)
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

        /// <summary>
        /// This method is called on the very end of every Save operation
        /// </summary>
        /// <param name="row">The row that was persisted</param>
        /// <param name="entity">The entity that was used as base for the save operation</param>
        protected virtual void AfterSave(TRow row, TEntity entity)
        {
        }

        #region update
        /// <summary>
        /// This method is called at the beginning of the Update operation (after the BeforeSave method)
        /// </summary>
        /// <param name="row">The row that will be persisted</param>
        /// <param name="entity">The entity that will be used as base for the update operation</param>
        protected virtual void BeforeUpdate(TRow row, TEntity entity)
        {
        }

        /// <summary>
        /// <para>
        /// This method will be called when on Save, the entity could be found on the database
        /// therefore updating an existing entry
        /// </para>
        /// </summary>
        /// <param name="row">The row that is going to be updated</param>
        /// <param name="entity">The entity which hold the new data</param>
        /// <param name="context">The context in which the operation is going to take place</param>
        protected virtual void Update(TRow row, TEntity entity, DataContext context)
        {
            this.BeforeSave(row, entity);
            this.BeforeUpdate(row, entity);

            context.SubmitChanges();

            this.AfterUpdate(row, entity);
            this.AfterSave(row, entity);
        }

        /// <summary>
        /// This method is called at the end of the Update operation (before the AfterSave method)
        /// </summary>
        /// <param name="row">The new row that was persisted</param>
        /// <param name="entity">The entity that was used as base for the update operation</param>
        protected virtual void AfterUpdate(TRow row, TEntity entity)
        {
        }
        #endregion

        #region create
        /// <summary>
        /// This method is called at the beginning of the Create operation (after the BeforeSave method)
        /// </summary>
        /// <param name="row">The new row that will be persisted</param>
        /// <param name="entity">The entity that will be used as base for the create operation</param>
        protected virtual void BeforeCreate(TRow row, TEntity entity)
        {
        }

        /// <summary>
        /// <para>
        /// This method will be called when on Save, the entity could not be found on the database
        /// therefore creating a new entry
        /// </para>
        /// </summary>
        /// <param name="entity">The entity which hold the new data</param>
        /// <param name="context">The context in which the operation is going to take place</param>
        protected virtual void Create(TEntity entity, DataContext context)
        {
            TRow row = (TRow)this.RowFactory.Create(typeof(TRow));
            Table<TRow> table = (Table<TRow>)this.RowFactory.CreateTable(typeof(TRow), context);

            this.BeforeSave(row, entity);
            this.BeforeCreate(row, entity);

            table.InsertOnSubmit(row);
            context.SubmitChanges();

            this.AfterCreate(row, entity);
            this.AfterSave(row, entity);
        }

        /// <summary>
        /// This method is called at the end of the Create operation (before the AfterSave method)
        /// </summary>
        /// <param name="row">The new row that was persisted</param>
        /// <param name="entity">The entity that was used as base for the create operation</param>
        protected virtual void AfterCreate(TRow row, TEntity entity)
        {
        }
        #endregion
        #endregion

        #region find
        /// <summary>
        /// <remarks>
        /// This method must be overriden
        /// </remarks>
        /// <para>
        /// This method should define the criteria of how a given entity
        /// will be sought on the database so that the repository can
        /// decide if it should update an existing entry or create a new one
        /// when the method Save is called
        /// </para>
        /// </summary>
        /// <param name="entity">The entity that will be sought</param>
        /// <param name="context">The context in which the search is going to take place</param>
        /// <returns></returns>
        protected abstract TRow FindEntity(TEntity entity, DataContext context);

        /// <summary>
        /// This method finds a single entry of a given entity on the database
        /// </summary>
        /// <param name="exp">The expression to be evaluated</param>
        /// <returns>
        /// If more than one entry is found, an exception will be thrown. 
        /// If no entries can be found, it will return null
        /// </returns>
        public virtual TEntity FindSingle(Func<TRow, bool> exp)
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

        /// <summary>
        /// This method finds a list of entries of a given entity on the database
        /// </summary>
        /// <param name="exp">The expression to be evaluated</param>
        /// <returns>
        /// If no entries can be found, it will return an empty list
        /// </returns>
        public virtual IList<TEntity> Find(Func<TRow, bool> exp)
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

        /// <summary>
        /// This method finds a single entry of a given entity on the database
        /// on a specific context
        /// </summary>
        /// <param name="exp">The expression to be evaluated</param>
        /// <param name="context">The specific context in which the operation will take place</param>
        /// <returns>
        /// If more than one entry is found, an exception will be thrown. 
        /// If no entries can be found, it will return null
        /// </returns>
        protected virtual TRow FindSingle(Func<TRow, bool> exp, DataContext context)
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

        /// <summary>
        /// This method finds a list of entries of a given entity on the database
        /// on a specific context
        /// </summary>
        /// <param name="exp">The expression to be evaluated</param>
        /// <param name="context">The specific context in which the operation will take place</param>
        /// <returns>
        /// If no entries can be found, it will return an empty list
        /// </returns>
        protected virtual IList<TRow> Find(Func<TRow, bool> exp, DataContext context)
        {
            return context.GetTable<TRow>().Where(exp).ToList();
        }
        #endregion

        /// <summary>
        /// This method converts a list of database entries to its specific kind of entity
        /// using the EntityFactory configured for the repository
        /// </summary>
        /// <param name="list">The list of rows to be converted</param>
        /// <returns>The corresponding list of entities</returns>
        protected virtual IList<TEntity> ToEntity(IList<TRow> list)
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
