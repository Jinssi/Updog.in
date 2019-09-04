using System.Linq;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a post into it's data transfer object.
    /// </summary>
    public sealed class PostViewMapper : IMapper<Post, PostView> {
        #region Fields
        /// <summary>
        /// Mapper to convert a user to its DTO.
        /// </summary>
        private IMapper<User, UserView> userMapper;

        private IMapper<Space, SpaceView> spaceMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post view mapper.
        /// </summary>
        /// <param name="userMapper">The mapper to convert users to user views.</param>
        /// <param name="spaceMapper">The mapper to convert space entities</param>
        public PostViewMapper(IMapper<User, UserView> userMapper, IMapper<Space, SpaceView> spaceMapper) {
            this.userMapper = userMapper;
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        public PostView Map(Post post) {
            UserView userView = userMapper.Map(post.User);
            SpaceView spaceView = spaceMapper.Map(post.Space);
            return new PostView(post.Id, post.Type, post.Title, post.Body, userView, spaceView, post.CreationDate, post.CommentCount, post.WasUpdated, post.WasDeleted);
        }
        #endregion
    }
}