using System.Collections.Generic;
using System.Linq;
using StarterKit.Framework.Exceptions;

namespace StarterKit.Framework.Validation
{
    public class ValidationResult<T>
    {
        private readonly List<Validation> _errors = new List<Validation>();

        public ValidationResult()
            : this(default(T))
        {
        }

        public ValidationResult(T data, bool isValid = false, params Validation[] errors)
        {
            Data = data;
            IsValid = isValid;

            if (errors != null)
            {
                _errors.AddRange(errors);
            }
        }

        public T Data { get; set; }

        public bool IsValid { get; set; }

        public ICollection<Validation> Errors
        {
            get
            {
                return _errors;
            }
        }

        public ValidationResult<T> AddError(string errorItemName, string errorMessage, ErrorCode errorCode, params object[] args)
        {
            if ((args != null) && args.Any())
            {
                errorMessage = string.Format(errorMessage, args);
            }

            _errors.Add(new Validation(errorItemName, errorMessage, errorCode));
            IsValid = false;

            return this;
        }

        public ValidationResult<T> AddErrors(IEnumerable<Validation> errors)
        {
            var errorList = errors == null ? null : errors.ToList();
            if (errors != null && errorList.Any())
            {
                foreach (var error in errorList)
                {
                    _errors.Add(error);
                }

                IsValid = false;
            }

            return this;
        }

        public void ThowIfInvalid(string errorMessage = "")
        {
            if (!IsValid)
                throw new ValidationException(errorMessage, Errors);
        }
    }
}
