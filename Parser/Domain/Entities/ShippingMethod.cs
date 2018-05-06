using System.Diagnostics.CodeAnalysis;

namespace Parser.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class ShippingMethod : AuditableEntity
    {
        public string Name { get; set; }
    }
}
