using System;
using System.Collections.Generic;
using System.Linq;

namespace StarterKit.Framework.Exceptions
{
    public class ServiceUnavailableException : Exception
    { 
        private readonly ICollection<ErrorInfo> _errors;

        public ServiceUnavailableException()
        {
            _errors = new List<ErrorInfo>();
        }

        public ServiceUnavailableException(ErrorInfo error)
        {
            _errors = new List<ErrorInfo> { error };
        }

        public ServiceUnavailableException(ICollection<ErrorInfo> errors)
        {
            _errors = errors;
        }

        public ServiceUnavailableException(string errorItem, string errorMessage, ErrorCode errorCode, params object[] args)
        {
            if ((args != null) && args.Any())
            {
                errorMessage = string.Format(errorMessage, args);
            }

            _errors = new List<ErrorInfo> { new ErrorInfo(errorItem, errorMessage, errorCode) };
        }

        public ServiceUnavailableException(string message, ICollection<ErrorInfo> errors)
            : base(message)
        {
            _errors = errors;
        }

        public ServiceUnavailableException(ICollection<ErrorInfo> errors, Exception innerException)
            : base(null, innerException)
        {
            _errors = errors;
        }

        public ServiceUnavailableException(string message, ICollection<ErrorInfo> errors, Exception innerException)
            : base(message, innerException)
        {
            _errors = errors;
        }

        public ICollection<ErrorInfo> Errors
        {
            get
            {
                return _errors;
            }
        }
    }
}
