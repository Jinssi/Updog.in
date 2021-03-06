using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Updog.Application;

namespace Updog.Application {
    /// <summary>
    /// Base class for validators to implement.
    /// </summary>
    public abstract class FluentValidatorAdapter<TResource> : AbstractValidator<TResource>, Application.IValidator<TResource> {
        #region Publics
        public new async Task<ValidationResult> ValidateAsync(TResource resource, CancellationToken token = default(CancellationToken)) {
            var result = await base.ValidateAsync(resource);
            return MapToResult(result);
        }

        public new ValidationResult Validate(TResource resource) {
            var result = base.Validate(resource);
            return MapToResult(result);
        }

        public async Task<ValidationResult> ValidateAsync(object resource, CancellationToken token = default(CancellationToken)) => await ValidateAsync((TResource)resource, token);

        public ValidationResult Validate(object resource) => Validate((TResource)resource);

        public bool CanValidate(object obj) => obj.GetType() == typeof(TResource);
        #endregion

        #region Helpers
        private ValidationResult MapToResult(FluentValidation.Results.ValidationResult fluentResult) {
            var errors = fluentResult.Errors.Select(e => new ValidationFailure(e.PropertyName, e.ErrorMessage));

            if (errors.Count() == 0) {
                return ValidationResult.Success();
            } else {
                return ValidationResult.Fail(errors);
            }
        }
        #endregion
    }
}