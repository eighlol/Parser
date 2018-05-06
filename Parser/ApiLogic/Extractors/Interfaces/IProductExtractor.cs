using System.Collections.Generic;
using Parser.AppData.Models;
using Parser.Domain.Entities;

namespace Parser.ApiLogic.Extractors.Interfaces
{
    public interface IProductExtractor
    {
        IEnumerable<Product> ExtractProducts(Products products);
    }
}