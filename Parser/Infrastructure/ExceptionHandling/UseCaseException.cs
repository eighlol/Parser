using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;

namespace Parser.Infrastructure.ExceptionHandling
{
    [ExcludeFromCodeCoverage]
    public class UseCaseException : Exception
    {
        public UseCaseExceptionCode StatusCode { get; set; }

        public string ContentType { get; set; } = @"text/plain";

        public UseCaseException(UseCaseExceptionCode statusCode)
        {
            StatusCode = statusCode;
        }

        public UseCaseException(UseCaseExceptionCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public UseCaseException(UseCaseExceptionCode statusCode, Exception inner) : this(statusCode, inner.ToString()) { }

        public UseCaseException(UseCaseExceptionCode statusCode, JObject errorObject) : this(statusCode, errorObject.ToString())
        {
            ContentType = @"application/json";
        }
    }
}
