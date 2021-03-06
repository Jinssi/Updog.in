using Updog.Domain;
using FluentValidation;
using Updog.Application;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate new comments being created.
    /// </summary>
    public sealed class CommentCreateCommandValidator : FluentValidatorAdapter<CommentCreateCommand> {
        #region Constructor(s)
        public CommentCreateCommandValidator() {
            RuleFor(c => c.Data.PostId).GreaterThan(0).WithMessage("Post Id is required.");

            RuleFor(c => c.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(c => c.Data.Body).NotNull().WithMessage("Body is required.");
            RuleFor(c => c.Data.Body).NotNull().WithMessage("Body is required.");
            RuleFor(c => c.Data.Body).NotEmpty().WithMessage("Body is required.");
            RuleFor(c => c.Data.Body).MaximumLength(Comment.BodyMaxLength).WithMessage($"Body must be {Comment.BodyMaxLength} characters or less.");
        }
        #endregion
    }
}