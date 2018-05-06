using System;
using System.Net;
using Parser.Infrastructure.ExceptionHandling;

namespace Parser.Middleware
{
    public interface IUseCaseExceptionCodeMapper
    {
        HttpStatusCode MapToStatusCode(UseCaseExceptionCode code);
    }
}
