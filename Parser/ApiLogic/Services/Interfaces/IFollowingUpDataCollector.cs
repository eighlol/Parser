using System.Collections.Generic;
using Parser.AppData.Models;
using Parser.Domain.Entities;

namespace Parser.ApiLogic.Services.Interfaces
{
    public interface IFollowingUpDataCollector
    {
        IEnumerable<Product> CollectData();
    }
}
