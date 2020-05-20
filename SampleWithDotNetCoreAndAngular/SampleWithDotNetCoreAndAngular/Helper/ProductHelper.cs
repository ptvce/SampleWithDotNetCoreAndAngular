using Dapper;
using Microsoft.Extensions.Configuration;
using SampleWithDotNetCoreAndAngular.Common;
using SampleWithDotNetCoreAndAngular.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Helper
{
    
    public interface IProductHelper
    {
       Task AddAsync(ProductModel model);
        Task<ProductModel> AddWithSPAsync(ProductModel model);

        Task<List<ProductModel>> GetAllAsync();
       Task<ProductModel> GetAsync(int id);
        Task<(List<ProductModel>,List<CategoryModel>)> GetMultipleProduct_CategoryAsync();
    }

    public class ProductHelper : IProductHelper
    {
        IConfiguration _configuration;
        ISqlUtility _sqlUtility;
        public ProductHelper(IConfiguration configuration, ISqlUtility sqlUtility)
        {
            _configuration = configuration;
            _sqlUtility = sqlUtility;
    }
        public async Task AddAsync(ProductModel model)
        {
            using (IDbConnection dbConnection = _sqlUtility.GetNewConnection())
            { 
                string query = "Insert into Products(ProductName,CategoryId,UnitPrice) Values(@ProductName,@CategoryId,@UnitPrice)";
                var parameters = new DynamicParameters();
                parameters.Add("ProductName", model.ProductName);
                parameters.Add("CategoryId",model.CategoryId);
                parameters.Add("UnitPrice", model.UnitPrice);

                await dbConnection.ExecuteAsync(query,parameters);
            }

        }

        public async Task<ProductModel> AddWithSPAsync(ProductModel model)
        {
            using (IDbConnection dbConnection = _sqlUtility.GetNewConnection())
            {
                string query = "SP_Products_Insert";
                var parameters = new DynamicParameters();
                parameters.Add("ProductName", model.ProductName);
                parameters.Add("CategoryId", model.CategoryId);
                parameters.Add("UnitPrice", model.UnitPrice);

                parameters.Add("id",dbType: DbType.Int32,direction: ParameterDirection.Output);

                await dbConnection.ExecuteAsync(query, parameters,commandType: CommandType.StoredProcedure);
                model.ProductId = parameters.Get<int>("id");
                return model;
            }
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            using (IDbConnection dbConnection = _sqlUtility.GetNewConnection())
            {
                string query = "select ProductId,CategoryId,ProductName,UnitPrice from products";

                var result = await dbConnection.QueryAsync<ProductModel>(query);
                return result.ToList();
            }
        }

        public async Task<ProductModel> GetAsync(int id)
        {
            using (IDbConnection dbConnection = _sqlUtility.GetNewConnection())
            {
                string query = "SP_Products_GetById";
                var parameters = new DynamicParameters();
                parameters.Add("ProductId", id);

                var result = await dbConnection.QuerySingleOrDefaultAsync<ProductModel>(query, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
               
        }

        public async Task<(List<ProductModel>, List<CategoryModel>)> GetMultipleProduct_CategoryAsync()
        {
            using (IDbConnection dbConnection = _sqlUtility.GetNewConnection())
            {
                string query = "select * from products;select * from categories";
                var result = await dbConnection.QueryMultipleAsync(query);
                var products = await result.ReadAsync<ProductModel>();
                var categories = await result.ReadAsync<CategoryModel>();
                return (products.ToList(),categories.ToList());
            }
        }
    }
}
