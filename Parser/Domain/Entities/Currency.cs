using System.Diagnostics.CodeAnalysis;

namespace Parser.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Currency : BaseEntity
    {
        public string Code { get; set; }
    }
}
