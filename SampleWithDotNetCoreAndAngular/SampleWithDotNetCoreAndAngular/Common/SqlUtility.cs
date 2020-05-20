using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Common
{
    public interface ISqlUtility
    {
        SqlConnection GetNewConnection();
    }
    public class SqlUtility : ISqlUtility
    {
        IConfiguration _configuration;
        public SqlUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection GetNewConnection()
        {
            string connectionString = _configuration.GetConnectionString("CoreLearningDB");
            return new SqlConnection(connectionString);
        }
    }
}
