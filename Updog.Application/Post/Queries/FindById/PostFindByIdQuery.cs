using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindByIdQuery : AnonymousQuery {
        #region Properties
        public int PostId { get; set; }
        #endregion
    }
}