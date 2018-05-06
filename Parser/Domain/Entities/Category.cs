using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Parser.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Category : AuditableEntity
    {
        public string Name { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
