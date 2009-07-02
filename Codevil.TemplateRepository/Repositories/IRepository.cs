using System;
using System.Collections.Generic;
using Codevil.TemplateRepository.Entities;
using Codevil.TemplateRepository.Factories;
using Codevil.TemplateRepository.Controllers;
using System.Linq.Expressions;

namespace Codevil.TemplateRepository.Repositories
{
    /// <summary>
    /// Represents a repository, with methods to Save and Find entities
    /// </summary>
    /// <typeparam name="TRow">The type of database entry</typeparam>
    /// <typeparam name="TEntity">The type of entity</typeparam>
    public interface IRepository<TRow, TEntity>
        where TRow : class
        where TEntity : DataEntity
    {
        #region fields
        IDataContextFactory DataContextFactory { get; set; }
        IEntityFactory EntityFactory { get; set; }
        IRowFactory RowFactory { get; set; }
        bool AutoRollbackOnError { get; set; }
        #endregion

        #region find
        /// <summary>
        /// This method finds a list of entries of a given entity on the database
        /// </summary>
        /// <param name="exp">The expression to be evaluated</param>
        /// <returns>
        /// If no entries can be found, it will return an empty list
        /// </returns>
        IList<TEntity> Find(Expression<Func<TRow, bool>> exp);

        /// <summary>
        /// This method finds a single entry of a given entity on the database
        /// </summary>
        /// <param name="exp">The expression to be evaluated</param>
        /// <returns>
        /// If more than one entry is found, an exception will be thrown. 
        /// If no entries can be found, it will return null
        /// </returns>
        TEntity FindSingle(Expression<Func<TRow, bool>> exp);
        #endregion

        #region save
        /// <summary>
        /// <para>
        /// This method will persist (create or update) an entity on the database
        /// using an auto-commit transaction
        /// </para>
        /// </summary>
        /// <param name="entity">The entity that is going to be persisted</param>
        void Save(TEntity entity);

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
        void Save(TEntity entity, UnitOfWork unitOfWork);
        #endregion

        #region delete
        /// <summary>
        /// <para>
        /// This method will delete an entity from the database
        /// </para>
        /// </summary>
        /// <param name="entity">The entity that is going to be deleted</param>
        void Delete(TEntity entity);

        /// <summary>
        /// <para>
        /// This method will delete an entity from the database
        /// using an unit of work to handle the transaction
        /// </para>
        /// <para>
        /// You can call it multiple times, for different repositories and entities
        /// and as long as you pass the same instance of a unit of work as a parameter,
        /// all operations will be enclosed in the same transaction. After that, you
        /// can decide to commit or rollback the transaction
        /// </para>
        /// </summary>
        /// <param name="entity">The entity that is going to be deleted</param>
        /// <param name="unitOfWork">The unit of work in which the operation will take place</param>
        void Delete(TEntity entity, UnitOfWork unitOfWork);
        #endregion
    }
}
