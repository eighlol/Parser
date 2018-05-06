using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Parser.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Price : AuditableEntity
    {
        public decimal Value { get; set; }

        public Currency Currency { get; set; }

        [ForeignKey(nameof(Entities.Currency))]
        public Guid CurrencyId { get; set; }

        public decimal Discount { get; set; }
    }
}
