using System.Collections.Generic;
using System.Linq;
using Parser.ApiLogic.Extractors.Interfaces;
using Parser.AppData.Models;
using Parser.Domain.Entities;

namespace Parser.ApiLogic.Extractors
{
    public class ProductExtractor : IProductExtractor
    {
        public IEnumerable<Product> ExtractProducts(Products products)
        {
            return products.Product.Select(GetItem).ToList();
        }

        private static Product GetItem(ProductsProduct product)
        {
            return new Product
            {
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = ExtractPrice(product.Price),
                ProductShipping = ExtractProductShipping(product.Shipping),
                ProductCategories = ExtractProductCategories(product.Category)
            };
        }

        private static Price ExtractPrice(ProductsProductPrice price)
        {
            return new Price
            {
                Value = price.Value,
                Discount = price.Discount,
                Currency = new Currency
                {
                    Code = price.Currency
                }
            };
        }

        private static ProductShipping ExtractProductShipping(ProductsProductShipping productShipping)
        {
            return new ProductShipping
            {
                ShippingPrice = productShipping.Price,
                ShippingMethod = ExtractShippingMethod(productShipping.Method),
                Currency = new Currency()
                {
                    Code = productShipping.Currency
                }
            };
        }

        private static ShippingMethod ExtractShippingMethod(ProductsProductShippingMethod shippingMethod)
        {
            return new ShippingMethod()
            {
                Name = shippingMethod.Name
            };
        }

        private static List<ProductCategory> ExtractProductCategories(ProductsProductCategory productCategory)
        {
            return new List<ProductCategory>
            {
                new ProductCategory
                {
                    Category = new Category
                    {
                        Name = productCategory.Name
                    }
                }
            };
        }
    }
}