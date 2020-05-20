using Dapper;
using Microsoft.Extensions.Configuration;
using SampleWithDotNetCoreAndAngular.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Helper
{
    public interface ICategoryHelper
    {
        Task<List<CategoryModel>> GetAllAsync();
        Task<PaginationResultModel<CategoryModel>> GetWithPaginationAsync(int page=1,int limit=5);

    }
    public class CategoryHelper : ICategoryHelper
    {
        IConfiguration _configuration;
        public CategoryHelper(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public async Task<List<CategoryModel>> GetAllAsync()
        {
            string connectionString = _configuration.GetConnectionString("CoreLearningDB");
            using (IDbConnection dapper = new SqlConnection(connectionString))
            {
                string query = "Select CategoryId,CategoryName from Categories";
                var result = await dapper.QueryAsync<CategoryModel>(query);

                return result.ToList();
            }
        }

        public async Task<PaginationResultModel<CategoryModel>> GetWithPaginationAsync(int page=1, int limit=5)
        {
            var result = new PaginationResultModel<CategoryModel>();
            string connectionString = _configuration.GetConnectionString("CoreLearningDB");
            using (IDbConnection dapper = new SqlConnection(connectionString))
            {
                string query = "SP_Categories_GetAll_WithPagination";
                int skip = (page - 1) * limit;
                var parameters = new DynamicParameters();
                parameters.Add("skip",skip);
                parameters.Add("take",limit);
                parameters.Add("total",dbType: DbType.Int32, direction: ParameterDirection.Output);

                var queryResult= await dapper.QueryAsync<CategoryModel>(query,parameters,commandType: CommandType.StoredProcedure);
                var total = parameters.Get<int>("total");

                result.Data = queryResult.ToList();
                result.Page = page;
                result.Limit = limit;
                result.Total = total;
                result.Pages = Convert.ToInt32(Math.Ceiling((decimal)total / limit));

                return result;
            }
        }
    }
}
