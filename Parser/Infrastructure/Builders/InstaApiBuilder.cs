using System;
using InstaSharper.API;
using InstaSharper.Classes;
using InstaSharper.Logger;

namespace Parser.Infrastructure.Builders
{
    public static class InstaApiBuilder
    {
        public static IInstaApi Build()
        {
            var userSession = new UserSessionData
            {
                UserName = "username",
                Password = "password"
            };
            
            IInstaApi instaApi = InstaSharper.API.Builder.InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(LogLevel.Exceptions)) // use logger for requests and debug messages
                .SetRequestDelay(TimeSpan.FromSeconds(2))
                .Build();

            return instaApi;
        }
    }
}
