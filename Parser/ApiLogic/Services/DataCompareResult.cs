using System.Collections.Generic;
using Parser.Domain.Entities;

namespace Parser.ApiLogic.Services
{
    public class DataCompareResult
    {
        public IEnumerable<BaseEntity> ToUpload { get; set; }

        public IEnumerable<BaseEntity> ToEdit { get; set; }

        public IEnumerable<BaseEntity> ToDelete { get; set; }
    }
}