using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostDeleteCommandHandler : CommandHandler<PostDeleteCommand> {
        #region Fields
        private IPostService service;
        #endregion

        #region Constructor(s)
        public PostDeleteCommandHandler(IPostService service) {
            this.service = service;
        }
        #endregion

        #region Publics
        [Validate(typeof(PostDeleteCommandValidator))]
        [Policy(typeof(PostAlterCommandPolicy))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(PostDeleteCommand command) {
            if (!(await service.DoesPostExist(command.PostId))) {
                return new NotFoundError($"Post {command.PostId} does not exist.");
            }

            await service.Delete(command.PostId, command.User);
            return Success();
        }
        #endregion
    }
}