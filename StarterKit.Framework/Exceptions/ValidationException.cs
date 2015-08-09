using System;
using System.Collections.Generic;
using System.Linq;

namespace StarterKit.Framework.Exceptions
{
    public class ValidationException : Exception
    {
        private readonly ICollection<Validation.Validation> _errors;

        public ValidationException()
        {
            _errors = new List<Validation.Validation>();
        }

        public ValidationException(string message)
            : base(message)
        {
            _errors = new List<Validation.Validation>();
        }

        public ValidationException(Validation.Validation error)
        {
            _errors = new List<Validation.Validation> { error };
        }

        public ValidationException(ICollection<Validation.Validation> errors)
        {
            _errors = errors;
        }

        public ValidationException(string errorItem, string errorMessage, ErrorCode errorCode, params object[] args)
        {
            if ((args != null) && args.Any())
            {
                errorMessage = string.Format(errorMessage, args);
            }

            _errors = new List<Validation.Validation> { new Validation.Validation(errorItem, errorMessage, errorCode) };
        }

        public ValidationException(string message, ICollection<Validation.Validation> errors)
            : base(message)
        {
            _errors = errors;
        }

        public ValidationException(ICollection<Validation.Validation> errors, Exception innerException)
            : base(null, innerException)
        {
            _errors = errors;
        }

        public ValidationException(string message, ICollection<Validation.Validation> errors, Exception innerException)
            : base(message, innerException)
        {
            _errors = errors;
        }

        public ICollection<Validation.Validation> ValidationErrors
        {
            get
            {
                return _errors;
            }
        }
    }
}
