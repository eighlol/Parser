using System;
using System.Diagnostics.CodeAnalysis;

namespace Parser.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class ProductCategory : AuditableEntity
    {
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
