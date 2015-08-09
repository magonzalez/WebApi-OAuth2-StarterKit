using StarterKit.Framework.Exceptions;

namespace StarterKit.Framework.Validation
{
    public interface IValidationService<T>
    {
        /// <summary>
        /// Validates the given data and optionally throws a validation error if validation fails.
        /// </summary>
        /// <param name="data">The data to validate.</param>
        /// <param name="throwIfInvalid">Used to determine if a <see cref="ValidationException"/> be thrown if validation fails.</param>
        /// <param name="errorMessage">The errorMessage to use if a <see cref="ValidationException"/> is thrown.</param>
        /// <returns></returns>
        ValidationResult<T> Validate(T data, bool throwIfInvalid = false, string errorMessage = "");
    }
}
