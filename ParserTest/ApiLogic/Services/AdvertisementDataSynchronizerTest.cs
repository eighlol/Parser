using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Parser.ApiLogic.Services;
using Parser.ApiLogic.Services.Interfaces;
using Parser.AppData.Models;
using Parser.Domain.Entities;
using ParserTest.TestUtils;

namespace ParserTest.ApiLogic.Services
{
    [TestFixture(Category = TestCategory.UNIT_TEST)]
    public class AdvertisementDataSynchronizerTest
    {
        private Mock<IFollowingUpDataCollector> _followingUpDataCollectorMock;
        private Mock<IAdvertisementDataComparer> _advertisementDataComparerMock;
        private Mock<IAdvertisementItemDeletor> _advertisementItemDeletorMock;
        private Mock<IAdvertisementItemUpdater> _advertisementItemUpdaterMock;
        private Mock<IAdvertisementItemUploader> _advertisementItemUploaderMock;
        private IAdvertisementDataSynchronizer _advertisementDataSynchronizer;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _followingUpDataCollectorMock = new Mock<IFollowingUpDataCollector>();
            _advertisementDataComparerMock = new Mock<IAdvertisementDataComparer>();
            _advertisementItemDeletorMock = new Mock<IAdvertisementItemDeletor>();
            _advertisementItemUpdaterMock = new Mock<IAdvertisementItemUpdater>();
            _advertisementItemUploaderMock = new Mock<IAdvertisementItemUploader>();
            _advertisementDataSynchronizer = new AdvertisementDataSynchronizer(
                _followingUpDataCollectorMock.Object,
                _advertisementDataComparerMock.Object, 
                _advertisementItemDeletorMock.Object,
                _advertisementItemUpdaterMock.Object,
                _advertisementItemUploaderMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _followingUpDataCollectorMock.Reset();
            _advertisementDataComparerMock.Reset(); 
            _advertisementItemDeletorMock.Reset();
            _advertisementItemUpdaterMock.Reset();
            _advertisementItemUploaderMock.Reset();
        }

        [Test]
        public void SyncData_ShouldUpload()
        {
            var products = new List<Product>
            {
                new Product()
                {
                    Name = "Sole - Dover, Whole, Fresh",
                    Description = "Subcutaneous fat necrosis due to birth injury",
                    Price = new Price()
                    {
                        Value = 60,
                        Discount = 65,
                        Currency = new Currency()
                        {
                            Code = "IDR"
                        }
                    },
                    ProductCategories = new List<ProductCategory>
                    {
                        new ProductCategory()
                        {
                            Category = new Category
                            {
                                Name = "Tools"
                            }
                        }
                    },
                    ImageUrl = @"http://dummyimage.com/127x193.jpg/dddddd/000000",
                    ProductShipping = new ProductShipping()
                    {
                        Currency = new Currency()
                        {
                            Code = "CNY"
                        },
                        ShippingPrice = 14,
                        ShippingMethod = new ShippingMethod
                        {
                            Name = "PowerShares KBW Bank Portfolio"
                        }
                    }
                },
                new Product()
                {
                    Name = "Pepperoni Slices",
                    Description = "Rapidly progressive nephritic syndrome with unspecified morphologic changes",
                    Price = new Price()
                    {
                        Value = 15,
                        Discount = 80,
                        Currency = new Currency()
                        {
                            Code = "BAM"
                        }
                    },
                    ProductCategories = new List<ProductCategory>
                    {
                        new ProductCategory()
                        {
                            Category = new Category
                            {
                                Name = "Computers"
                            }
                        }
                    },
                    ImageUrl = @"http://dummyimage.com/225x231.jpg/cc0000/ffffff",
                    ProductShipping = new ProductShipping()
                    {
                        Currency = new Currency()
                        {
                            Code = "MKD"
                        },
                        ShippingPrice = 63,
                        ShippingMethod = new ShippingMethod
                        {
                            Name = "Rapidly progressive nephritic syndrome with unspecified morphologic changes"
                        }
                    }
                },
                new Product()
                {
                    Name = "Raisin - Golden",
                    Description = "Postprocedural ovarian failure",
                    Price = new Price()
                    {
                        Value = 41,
                        Discount = 11,
                        Currency = new Currency()
                        {
                            Code = "RSD"
                        }
                    },
                    ProductCategories = new List<ProductCategory>
                    {
                        new ProductCategory()
                        {
                            Category = new Category
                            {
                                Name = "Electronics"
                            }
                        }
                    },
                    ImageUrl = @"http://dummyimage.com/150x136.jpg/5fa2dd/ffffff",
                    ProductShipping = new ProductShipping()
                    {
                        Currency = new Currency()
                        {
                            Code = "IRR"
                        },
                        ShippingPrice = 58,
                        ShippingMethod = new ShippingMethod
                        {
                            Name = "Brady Corporation"
                        }
                    }
                }
            };

            var compareResult = new DataCompareResult
            {
                ToUpload = products
            };

            _followingUpDataCollectorMock.Setup(fudc => fudc.CollectData())
                .Returns(products);

            _advertisementDataComparerMock.Setup(adc => adc.Compare(products))
                .Returns(compareResult);
            
            //ACT
            _advertisementDataSynchronizer.SyncData();

            //ASSERT
            _advertisementItemUploaderMock.Verify(aiu => aiu.Upload(It.IsAny<BaseEntity>()), Times.Exactly(3));
        }
    }
}
