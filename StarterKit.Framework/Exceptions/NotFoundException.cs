using System;

namespace StarterKit.Framework.Exceptions
{
    public class NotFoundException : Exception
    {
        private readonly object _reference;

        public NotFoundException(object reference)
        {
            _reference = reference;
        }

        public NotFoundException(string message, object reference) : base(message)
        {
            _reference = reference;
        }

        public NotFoundException(object reference, Exception innerException) : base(null, innerException)
        {
            _reference = reference;
        }

        public NotFoundException(string message, object reference, Exception innerException)
            : base(message, innerException)
        {
            _reference = reference;
        }

        public object Reference
        {
            get
            {
                return _reference;
            }
        }
    }
}
