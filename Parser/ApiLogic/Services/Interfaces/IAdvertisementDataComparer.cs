using System.Collections.Generic;
using Parser.Domain.Entities;

namespace Parser.ApiLogic.Services.Interfaces
{
    public interface IAdvertisementDataComparer
    {
        DataCompareResult Compare(IEnumerable<BaseEntity> data);
    }
}
