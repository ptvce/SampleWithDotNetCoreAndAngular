using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFisrtSampleProject.Entities
{
    public class Province
    {
        public int Id { get; set; }
        public string ProvinceName { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
    }
}
