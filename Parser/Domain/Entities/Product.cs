using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Parser.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Product : AuditableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Price Price { get; set; }

        [ForeignKey(nameof(Entities.Price))]
        public Guid PriceId { get; set; }

        public ProductShipping ProductShipping { get; set; }

        [ForeignKey(nameof(Entities.ProductShipping))]
        public Guid ProductShippingId { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public string ImageUrl { get; set; }
    }
}
