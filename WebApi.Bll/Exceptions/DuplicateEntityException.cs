using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Bll.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException() : base() { }

        public DuplicateEntityException(string message) : base(message) { }

        public DuplicateEntityException(string message, Exception innerException) : base(message, innerException) { }
    }
}
