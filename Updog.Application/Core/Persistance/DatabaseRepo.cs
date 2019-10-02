using System.Data.Common;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public abstract class DatabaseRepo {
        #region Properties
        protected DbConnection Connection { get; }
        #endregion

        #region Constructor(s)
        public DatabaseRepo(DbConnection connection) {
            Connection = connection;
        }
        #endregion
    }

    /// <summary>
    /// CRUD interface for managing users.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity stored.</typeparam>
    public abstract class DatabaseRepo<TEntity> : DatabaseRepo where TEntity : class, IEntity {
        #region Constructor(s)
        public DatabaseRepo(DbConnection connection) : base(connection) { }
        #endregion

        #region Publics
        /// <summary>
        /// Find an entity by it's unique Id.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <returns>The matching entity found.</returns>
        public abstract Task<TEntity?> FindById(int id);

        /// <summary>
        /// Add a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public abstract Task Add(TEntity entity);


        /// <summary>
        /// Update an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public abstract Task Update(TEntity entity);

        /// <summary>
        /// Delete an existing entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public abstract Task Delete(TEntity entity);
        #endregion
    }
}