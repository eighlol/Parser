using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Parser.Infrastructure.ExceptionHandling;
using Parser.Middleware;

namespace ParserTest.Middleware
{
    [TestFixture(Category = TestCategory.UNIT_TEST)]
    public class ExceptionMiddlewareTest
    {
        private Mock<RequestDelegate> requestDelegateMock;
        private Mock<IUseCaseExceptionCodeMapper> useCaseExceptionCodeMapperMock;
        private Mock<ILogger<ExceptionMiddleware>> loggerMock;
        private ExceptionMiddleware _exceptionMiddleware;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            requestDelegateMock = new Mock<RequestDelegate>();
            useCaseExceptionCodeMapperMock = new Mock<IUseCaseExceptionCodeMapper>();
            loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
            _exceptionMiddleware = new ExceptionMiddleware(requestDelegateMock.Object,
                useCaseExceptionCodeMapperMock.Object, loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public async Task Invoke_ShouldCatchException()
        {
            //ARRANGE
            var useCaseException = new UseCaseException(UseCaseExceptionCode.FailedToAuthenticatedToInstagram);
            var defaultHttpContext = new DefaultHttpContext();
            const HttpStatusCode httpStatusCode = HttpStatusCode.Unauthorized;

            requestDelegateMock.Setup(rd => rd(defaultHttpContext)).Throws(useCaseException);

            useCaseExceptionCodeMapperMock.Setup(ucecm => ucecm.MapToStatusCode(useCaseException.StatusCode))
                .Returns(httpStatusCode);

            //ACT
            await _exceptionMiddleware.Invoke(defaultHttpContext);

            defaultHttpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(defaultHttpContext.Response.Body, Encoding.UTF8))
            {
                var responseBody = await reader.ReadToEndAsync();

                //ASSERT
                Assert.AreEqual(defaultHttpContext.Response.ContentType, useCaseException.ContentType);

                Assert.AreEqual(responseBody, useCaseException.Message);
                Assert.AreEqual(defaultHttpContext.Response.StatusCode, (int) httpStatusCode);
            }
        }
    }
}