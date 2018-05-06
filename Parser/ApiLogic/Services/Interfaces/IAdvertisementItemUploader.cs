using Parser.Domain.Entities;

namespace Parser.ApiLogic.Services.Interfaces
{
    public interface IAdvertisementItemUploader
    {
        void Upload(BaseEntity enity);
    }
}