using Parser.AppData.Models;

namespace Parser.ApiLogic.Services.Interfaces
{
    public interface IExternalClientDataProvider
    {
        Products GetData();
    }
}