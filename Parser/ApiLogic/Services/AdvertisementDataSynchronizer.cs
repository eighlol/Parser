using Parser.ApiLogic.Services.Interfaces;
using Parser.Domain.Entities;
using System.Collections.Generic;

namespace Parser.ApiLogic.Services
{
    public class AdvertisementDataSynchronizer : IAdvertisementDataSynchronizer
    {
        private readonly IFollowingUpDataCollector _followingUpDataCollector;
        private readonly IAdvertisementDataComparer _advertisementDataComparer;
        private readonly IAdvertisementItemDeletor _advertisementItemDeletor;
        private readonly IAdvertisementItemUpdater _advertisementItemUpdater;
        private readonly IAdvertisementItemUploader _advertisementItemUploader;

        public AdvertisementDataSynchronizer(
            IFollowingUpDataCollector followingUpDataCollector,
            IAdvertisementDataComparer advertisementDataComparer,
            IAdvertisementItemDeletor advertisementItemDeletor,
            IAdvertisementItemUpdater advertisementItemUpdater,
            IAdvertisementItemUploader advertisementItemUploader)
        {
            _followingUpDataCollector = followingUpDataCollector;
            _advertisementDataComparer = advertisementDataComparer;
            _advertisementItemDeletor = advertisementItemDeletor;
            _advertisementItemUpdater = advertisementItemUpdater;
            _advertisementItemUploader = advertisementItemUploader;
        }

        public void SyncData()
        {
            IEnumerable<Product> data = _followingUpDataCollector.CollectData();

            var dataCompareResult = _advertisementDataComparer.Compare(data);

            foreach (var product in dataCompareResult.ToUpload)
            {
                _advertisementItemUploader.Upload(product);
            }
        }
    }
}