using Moq;
using NUnit.Framework;
using Parser.ApiLogic.Extractors.Interfaces;
using Parser.ApiLogic.Services;
using Parser.ApiLogic.Services.Interfaces;
using Parser.AppData.Models;
using Parser.Domain.Entities;
using ParserTest.TestUtils;
using System.Collections.Generic;

namespace ParserTest.ApiLogic.Extractor
{
    [TestFixture(Category = TestCategory.UNIT_TEST)]
    public class FollowingUpDataCollectorTest
    {
        private Mock<IExternalClientDataProvider> _externalClientDataProviderMock;
        private Mock<IProductExtractor> _productExtractorMock;
        private IFollowingUpDataCollector _followingUpDataCollector;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _externalClientDataProviderMock = new Mock<IExternalClientDataProvider>();
            _productExtractorMock = new Mock<IProductExtractor>();
            _followingUpDataCollector = new FollowingUpDataCollector(_externalClientDataProviderMock.Object, _productExtractorMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _externalClientDataProviderMock.Reset();
        }

        [Test]
        public void CollectData_ShouldSuccess()
        {
            var products = new Products
            {
                Product = new[]
                {
                    new ProductsProduct
                    {
                        id = 1,
                        Name = "Sole - Dover, Whole, Fresh",
                        Description = "Subcutaneous fat necrosis due to birth injury",
                        Price = new ProductsProductPrice
                        {
                            Value = 60,
                            Discount = 65,
                            Currency = "IDR"
                        },
                        Category = new ProductsProductCategory
                        {
                            Name = "Tools"
                        },
                        ImageUrl = @"http://dummyimage.com/127x193.jpg/dddddd/000000",
                        Shipping = new ProductsProductShipping
                        {
                            Currency = "CNY",
                            Price = 14,
                            Method = new ProductsProductShippingMethod
                            {
                                Name = "PowerShares KBW Bank Portfolio"
                            }
                        }
                    },
                    new ProductsProduct
                    {
                        id = 2,
                        Name = "Pepperoni Slices",
                        Description = "Rapidly progressive nephritic syndrome with unspecified morphologic changes",
                        Price = new ProductsProductPrice
                        {
                            Value = 15,
                            Discount = 80,
                            Currency = "BAM"
                        },
                        Category = new ProductsProductCategory
                        {
                            Name = "Computers"
                        },
                        ImageUrl = @"http://dummyimage.com/225x231.jpg/cc0000/ffffff",
                        Shipping = new ProductsProductShipping
                        {
                            Currency = "MKD",
                            Price = 63,
                            Method = new ProductsProductShippingMethod
                            {
                                Name = "Rapidly progressive nephritic syndrome with unspecified morphologic changes"
                            }
                        }
                    },
                    new ProductsProduct
                    {
                        id = 3,
                        Name = "Raisin - Golden",
                        Description = "Postprocedural ovarian failure",
                        Price = new ProductsProductPrice
                        {
                            Value = 41,
                            Discount = 11,
                            Currency = "RSD"
                        },
                        Category = new ProductsProductCategory
                        {
                            Name = "Electronics"
                        },
                        ImageUrl = @"http://dummyimage.com/150x136.jpg/5fa2dd/ffffff",
                        Shipping = new ProductsProductShipping
                        {
                            Currency = "IRR",
                            Price = 58,
                            Method = new ProductsProductShippingMethod
                            {
                                Name = "Brady Corporation"
                            }
                        }
                    }
                }
            };

            var domainProducts = new List<Product>
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

            _externalClientDataProviderMock.Setup(ecdp => ecdp.GetData())
                .Returns(products);

            _productExtractorMock.Setup(pe => pe.ExtractProducts(products))
                .Returns(domainProducts);

            //ACT
            IEnumerable<Product> actual = _followingUpDataCollector.CollectData();

            ContentAssert.AreEqual(domainProducts, actual);
        }

        [Test]
        public void CollectData_ShouldSuccess2()
        {
            const string resourceTestFileName = "AdvertisementDataShort.xml";

            var products = ResourceFileProvider.GetParsedObjectFromResourceFile<Products>(resourceTestFileName);

            var domainProducts = new List<Product>
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

            _externalClientDataProviderMock.Setup(ecdp => ecdp.GetData())
                .Returns(products);

            _productExtractorMock.Setup(pe => pe.ExtractProducts(products))
                .Returns(domainProducts);

            //ACT
            IEnumerable<Product> actual = _followingUpDataCollector.CollectData();

            ContentAssert.AreEqual(domainProducts, actual);
        }
    }
}