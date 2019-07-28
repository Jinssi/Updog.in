using System;
using System.Threading.Tasks;
using Updog.Domain;
using Updog.Application;
using MySql.Data.MySqlClient;
using System.Data.Common;
using Dapper;

namespace Updog.Persistance {
    /// <summary>
    /// CRUD interface for managing user's in the database.
    /// </summary>
    public sealed class UserRepo : DatabaseRepo<User>, IUserRepo {
        #region Constructor(s)
        /// <summary>
        /// Create a new user repo.
        /// </summary>
        /// <param name="database">The database to query off.</param>
        public UserRepo(IDatabase database) : base(database) { }
        #endregion

        #region Publics
        /// <summary>
        /// Find a user by their id.
        /// </summary>
        /// <param name="id">The id to look for.</param>
        /// <returns>The user with the id.</returns>
        public async Task<User> FindById(int id) {
            using (DbConnection connection = GetConnection()) {
                return await connection.QueryFirstAsync<User>(
                    "SELECT * FROM User WHERE Id = @Id;",
                    new { Id = id }
                );
            }
        }

        /// <summary>
        /// Find a user by their username.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <returns>The user with the username.</returns>
        public async Task<User> FindByUsername(string username) {
            using (DbConnection connection = GetConnection()) {
                return await connection.QueryFirstOrDefaultAsync<User>(
                    "SELECT * FROM User WHERE Username = @Username;",
                    new { Username = username }
                );
            }
        }

        /// <summary>
        /// Find a user via their contact email.
        /// </summary>
        /// <param name="email">The email to look for.</param>
        /// <returns>The user found (if any).</returns>
        public async Task<User> FindByEmail(string email) {
            using (DbConnection connection = GetConnection()) {
                return await connection.QueryFirstOrDefaultAsync<User>(
                    "SELECT * FROM User WHERE Email = @Email;",
                    new { Email = email }
                );
            }
        }

        /// <summary>
        /// Add a new user to the database.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public async Task Add(User user) {
            using (DbConnection connection = GetConnection()) {
                user.Id = await connection.QueryFirstOrDefaultAsync<int>(
                    "INSERT INTO User (Username, Email, PasswordHash, JoinedDate) VALUES (@Username, @Email, @PasswordHash, @JoinedDate); SELECT LAST_INSERT_ID();",
                    user
                );
            }
        }

        /// <summary>
        /// Update a user in the database.
        /// </summary>
        /// <param name="user">The user to update.</param>
        public async Task Update(User user) {
            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync("UPDATE User SET Email = @Email, PasswordHash = @PasswordHash WHERE Id = @Id", user);
            }
        }

        /// <summary>
        /// Delete a user from the database.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        public async Task Delete(User user) {
            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync("DELETE FROM User WHERE Id = @Id", user);
            }
        }
        #endregion
    }
}