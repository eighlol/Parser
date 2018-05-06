using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Parser.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class ProductShipping : AuditableEntity
    {
        public decimal ShippingPrice { get; set; }

        public Currency Currency { get; set; }

        [ForeignKey(nameof(Entities.Currency))]
        public Guid CurrencyId { get; set; }

        public ShippingMethod ShippingMethod { get; set; }

        [ForeignKey(nameof(Entities.ShippingMethod))]
        public Guid ShippingMethodId { get; set; }
    }
}
