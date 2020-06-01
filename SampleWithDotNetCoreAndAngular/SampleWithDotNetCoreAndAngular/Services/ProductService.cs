using SampleWithDotNetCoreAndAngular.Common;
using SampleWithDotNetCoreAndAngular.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Services
{
    public interface IProductService
    {
        Task<ServicePaginationResult<ProductViewModel>> GetAll(int? page, int? limit);
    }
    public class ProductService
    {
    }
}
