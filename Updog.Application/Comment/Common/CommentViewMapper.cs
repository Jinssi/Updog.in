using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a comment to a comment view.
    /// </summary>
    public sealed class CommentViewMapper : IMapper<Comment, CommentView> {
        #region Fields
        /// <summary>
        /// Mapper to convert a user to a user view.
        /// </summary>
        private IMapper<User, UserView> userMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment view mapper.
        /// </summary>
        /// <param name="userMapper">The user mapper.</param>
        public CommentViewMapper(IMapper<User, UserView> userMapper) {
            this.userMapper = userMapper;
        }
        #endregion

        #region Publics2
        public CommentView Map(Comment comment) {
            UserView u = userMapper.Map(comment.User);
            CommentView cv = new CommentView(comment.Id, u, comment.Body);

            if (comment.Children.Count > 0) {
                foreach (Comment c in comment.Children) {
                    cv.Children.Add(Map(c));
                }
            }

            return cv;
        }
        #endregion
    }
}