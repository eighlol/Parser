using System.ComponentModel;
using System.Net;
using Parser.Infrastructure.ExceptionHandling;

namespace Parser.Middleware
{
    public class UseCaseExceptionCodeMapper : IUseCaseExceptionCodeMapper
    {
        public HttpStatusCode MapToStatusCode(UseCaseExceptionCode code)
        {
            switch (code)
            {
                case UseCaseExceptionCode.FailedToAuthenticatedToInstagram:
                    return HttpStatusCode.Unauthorized;
                default:
                    throw new InvalidEnumArgumentException(nameof(code), (int)code, typeof(UseCaseExceptionCode));
            }
        }
    }
}
