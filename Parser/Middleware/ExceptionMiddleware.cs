using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Parser.Infrastructure.ExceptionHandling;
using System.Threading.Tasks;

namespace Parser.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUseCaseExceptionCodeMapper _codeMapper;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, IUseCaseExceptionCodeMapper codeMapper, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _codeMapper = codeMapper;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UseCaseException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, ExceptionMiddleware will not be executed.");
                    throw;
                }
                
                context.Response.Clear();
                context.Response.StatusCode = (int)_codeMapper.MapToStatusCode(ex.StatusCode);
                context.Response.ContentType = ex.ContentType;

                await context.Response.WriteAsync(ex.Message);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HttpStatusCodeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
