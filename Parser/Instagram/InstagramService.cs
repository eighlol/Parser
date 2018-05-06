using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using InstaSharper.Logger;
using Parser.Instagram.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.API;
using LogLevel = InstaSharper.Logger.LogLevel;

namespace Parser.Instagram
{
    public class InstagramService : IInstagramService
    {
        private readonly IInstaApi _instaApi;

        public InstagramService(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        //private async Task Ensure()
        //{
        //    try
        //    {
        //       // create user session data and provide login details
        //        var userSession = new UserSessionData
        //        {
        //            UserName = "username",
        //            Password = "password"
        //        };

        //        // create new InstaApi instance using Builder
        //        _instaApi = InstaApiBuilder.CreateBuilder()
        //            .SetUser(userSession)
        //            .UseLogger(new DebugLogger(LogLevel.Exceptions)) // use logger for requests and debug messages
        //            .SetRequestDelay(TimeSpan.FromSeconds(2))
        //            .Build();

                
        //        if (!_instaApi.IsUserAuthenticated)
        //        {
        //            // login
        //            _logger.LogInformation($"Logging in as {userSession.UserName}");
        //            var logInResult = await _instaApi.LoginAsync();
        //            if (!logInResult.Succeeded)
        //            {
        //                _logger.LogError($"Unable to login: {logInResult.Info.Message}");

        //            }
        //        }
        //        var state = _instaApi.GetStateDataAsStream();
        //        using (var fileStream = File.Create(stateFile))
        //        {
        //            state.Seek(0, SeekOrigin.Begin);
        //            state.CopyTo(fileStream);
        //        }

        //        Console.WriteLine("Press 1 to start basic demo samples");
        //        Console.WriteLine("Press 2 to start upload photo demo sample");
        //        Console.WriteLine("Press 3 to start comment media demo sample");
        //        Console.WriteLine("Press 4 to start stories demo sample");
        //        Console.WriteLine("Press 5 to start demo with saving state of API instance");
        //        Console.WriteLine("Press 6 to start messaging demo sample");
        //        Console.WriteLine("Press 7 to start location demo sample");
        //        Console.WriteLine("Press 8 to start collections demo sample");

        //        var samplesMap = new Dictionary<ConsoleKey, IDemoSample>
        //        {
        //            [ConsoleKey.D1] = new Basics(_instaApi),
        //            [ConsoleKey.D2] = new UploadPhoto(_instaApi),
        //            [ConsoleKey.D3] = new CommentMedia(_instaApi),
        //            [ConsoleKey.D4] = new Stories(_instaApi),
        //            [ConsoleKey.D5] = new SaveLoadState(_instaApi),
        //            [ConsoleKey.D6] = new Messaging(_instaApi),
        //            [ConsoleKey.D7] = new LocationSample(_instaApi),
        //            [ConsoleKey.D8] = new CollectionSample(_instaApi)

        //        };
        //        var key = Console.ReadKey();
        //        Console.WriteLine(Environment.NewLine);
        //        if (samplesMap.ContainsKey(key.Key))
        //            await samplesMap[key.Key].DoShow();
        //        Console.WriteLine("Done. Press esc key to exit...");

        //        key = Console.ReadKey();
        //        return key.Key == ConsoleKey.Escape;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        // perform that if user needs to logged out
        //        // var logoutResult = Task.Run(() => _instaApi.LogoutAsync()).GetAwaiter().GetResult();
        //        // if (logoutResult.Succeeded) Console.WriteLine("Logout succeed");
        //    }
        //    return false;
        //}


        public bool IsUserAuthenticated { get; }

        public async Task<IResult<InstaLoginResult>> LoginAsync()
        {
            return await _instaApi.LoginAsync();
        }

        public async Task<IResult<bool>> LogoutAsync()
        {
            return await _instaApi.LogoutAsync();
        }

        public async Task<IResult<InstaMedia>> UploadPhotoAsync(InstaImage image, string caption)
        {
            return await _instaApi.UploadPhotoAsync(image, caption);
        }

        public async Task<IResult<bool>> DeleteMediaAsync(string mediaId, InstaMediaType mediaType)
        {
            return await _instaApi.DeleteMediaAsync(mediaId, mediaType);
        }

        public async Task<IResult<bool>> EditMediaAsync(string mediaId, string caption)
        {
            return await _instaApi.EditMediaAsync(mediaId, caption);
        }
    }
}