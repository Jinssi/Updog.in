namespace Updog.Application {
    /// <summary>
    /// Parameters for the pot finder by user interactor.
    /// </summary>
    public sealed class PostFinderByUserParam : IPagable {
        #region Properties
        /// <summary>
        /// The username of the user to look for.
        /// </summary>
        public string Username { get; }

        public int PageNumber { get; }

        public int PageSize { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new set of post finder by user params.
        /// </summary>
        /// <param name="userId">The user ID to look for.</param>
        /// <param name="pageNumber">Page index</param>
        /// <param name="pageSize">Page size</param>
        public PostFinderByUserParam(string username, int pageNumber, int pageSize) {
            Username = username;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        #endregion
    }
}