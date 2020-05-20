using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Models
{
    public class PaginationResultModel<T>
    {
        public List<T> Data { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
    }
}
