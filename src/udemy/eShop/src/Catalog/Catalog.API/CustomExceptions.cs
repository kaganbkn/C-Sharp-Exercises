using System;

namespace Catalog.API
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message, Exception innerEx = null) : base(message, innerEx)
        {
        }
    }
    public class IntegrationException : Exception
    {
        public string HttpStatusCode { get; set; }
        public IntegrationException(string message, Exception innerEx = null, string httpStatusCode = null) : base(message, innerEx)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
    public class ValidationException : Exception
    {
        public ValidationException(string message, Exception innerEx = null) : base(message, innerEx)
        {
        }
    }
    public class ConflictException : Exception
    {
        public ConflictException(string message, Exception innerEx = null) : base(message, innerEx)
        {
        }
    }
}
