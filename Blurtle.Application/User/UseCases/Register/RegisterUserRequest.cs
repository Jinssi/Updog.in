
namespace Blurtle.Application {
    /// <summary>
    /// Request object to create a new user with the system.
    /// </summary>
    public sealed class RegisterUserRequest {
        #region Properties
        /// <summary>
        /// The username they want.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The password they want to use.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// The contact email (if any).
        /// </summary>
        public string Email { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new user registration.
        /// </summary>
        /// <param name="username">The username they want.</param>
        /// <param name="password">The password they want to use.</param>
        /// <param name="email">The contact email (if any).</param>
        public RegisterUserRequest(string username, string password, string email = null) {
            Username = username;
            Password = password;
            Email = email;
        }
        #endregion
    }
}