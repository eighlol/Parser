using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using System.Threading.Tasks;

namespace Parser.Instagram.Interfaces
{
    public interface IInstagramService
    {
        bool IsUserAuthenticated { get; }
        
        Task<IResult<InstaLoginResult>> LoginAsync();
        
        Task<IResult<bool>> LogoutAsync();

        Task<IResult<InstaMedia>> UploadPhotoAsync(InstaImage image, string caption);
        
        Task<IResult<bool>> DeleteMediaAsync(string mediaId, InstaMediaType mediaType);

        Task<IResult<bool>> EditMediaAsync(string mediaId, string caption);
    }
}
