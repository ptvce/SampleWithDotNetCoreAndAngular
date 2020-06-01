using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Models
{
    public class PaginationModel
    {
        public int Pages { get; set; }
        public int Page { get; set; }
        public string Url { get; set; }
    }
}
