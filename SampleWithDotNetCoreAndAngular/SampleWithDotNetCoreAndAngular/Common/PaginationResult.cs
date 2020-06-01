using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Common
{
    public class PaginationResult<T>
    {
        public IQueryable<T> Data { get; set; }
        public int? Page { get; set; }
        public int? Pages { get; set; }
        public int? Limit { get; set; }

        public int Total { get; set; }
    }
    public class ServicePaginationResult<T>
    {
        public IReadOnlyList<T> Data { get; set; }
        public int? Page { get; set; }
        public int? Pages { get; set; }
        public int? Limit { get; set; }

        public int Total { get; set; }
    }
}
