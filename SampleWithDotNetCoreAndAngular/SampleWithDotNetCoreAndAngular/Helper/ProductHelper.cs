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
    
    public interface IProductHelper
    {
       Task AddAsync(ProductModel model);
       Task <List<ProductModel>> GetAllAsync();
    }
    public class ProductHelper : IProductHelper
    {
        IConfiguration _configuration;
        public ProductHelper(IConfiguration configuration)
        {
            _configuration = configuration;
    }
        public async Task AddAsync(ProductModel model)
        {
           string connectionString = _configuration.GetConnectionString("CoreLearningDB");
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                string query = "Insert into Products(ProductName,CategoryId,UnitPrice) Values(@ProductName,@CategoryId,@UnitPrice)";
                var parameters = new DynamicParameters();
                parameters.Add("ProductName", model.ProductName);
                parameters.Add("CategoryId",model.CategoryId);
                parameters.Add("UnitPrice", model.UnitPrice);

                await dbConnection.ExecuteAsync(query,parameters);
            }

        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            string connectionString = _configuration.GetConnectionString("CoreLearningDB");
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                string query = "select ProductId,CategoryId,ProductName,UnitPrice from products";

                var result = await dbConnection.QueryAsync<ProductModel>(query);
                return result.ToList();
            }
        }
    }
}
