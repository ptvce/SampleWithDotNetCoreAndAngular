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
    }
}
