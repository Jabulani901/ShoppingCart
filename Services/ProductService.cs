using Dapper;
using Microsoft.Data.SqlClient;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{
    public class ProductService : IProductService
    {
        private readonly string connectionString = "Server=;Database=Login;Trusted_Connection=True;";

        public async Task<IEnumerable<Product>> GetProduct()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                return await sqlConnection.QueryAsync<Product>(
                    "spGetAllProduct",
                    null,
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task AddProduct(Product product)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ProductId", product.ProductId);
                dynamicParameters.Add("@ProductName", product.ProductName);
                dynamicParameters.Add("@ProductDescription", product.ProductDescription);
                dynamicParameters.Add("@ProductPrice", product.ProductPrice);

                await sqlConnection.ExecuteAsync(
                    "spAddProduct",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
