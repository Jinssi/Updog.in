using System.Threading.Tasks;
using Blurtle.Domain;
using FluentValidation;

namespace Blurtle.Application {
    /// <summary>
    /// Adds new posts to the system.
    /// </summary>
    public sealed class PostCreator : IInteractor<PostCreateParams, Post> {
        #region Fields
        private IPostRepo postRepo;

        private AbstractValidator<PostCreateParams> postValidator;
        #endregion

        #region Constructor(s)
        public PostCreator(IPostRepo postRepo, AbstractValidator<PostCreateParams> postValidator) {
            this.postRepo = postRepo;
            this.postValidator = postValidator;
        }
        #endregion

        public async Task<Post> Handle(PostCreateParams input) {
            await postValidator.ValidateAndThrowAsync(input);

            Post post = new Post(input.Type, input.Title, input.Body, input.User.Id);

            await postRepo.Add(post);
            return post;
        }
    }
}