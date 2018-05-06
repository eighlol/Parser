using Parser.ApiLogic.Extractors.Interfaces;
using Parser.ApiLogic.Services.Interfaces;
using Parser.AppData.Models;
using Parser.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Parser.ApiLogic.Services
{
    public class FollowingUpDataCollector : IFollowingUpDataCollector
    {
        private readonly IExternalClientDataProvider _externalClientDataProvider;
        private readonly IProductExtractor _productExtractor;

        public FollowingUpDataCollector(IExternalClientDataProvider externalClientDataProvider, IProductExtractor productExtractor)
        {
            _externalClientDataProvider = externalClientDataProvider;
            _productExtractor = productExtractor;
        }

        public IEnumerable<Product> CollectData()
        {
            Products products = _externalClientDataProvider.GetData();

            return _productExtractor.ExtractProducts(products).ToList();
        }
    }
}
