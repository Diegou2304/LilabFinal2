using Lilab.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Dapper;

namespace Lilab.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(IOptions<ConnectionStringConfig> connectionString)
        {
            _connectionString = connectionString.Value.Value;
        }
        public async Task<int> UpdateProduct(Product product)
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"update products set Stock= @newStock where id=@id";

            var param = new
            {
                newStock = product.Stock,
                id = product.Id,
            };
           
            return await connection.ExecuteAsync(query, param);
         
        }

        public async Task<Product> GetProductById(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"select * from products where ID=1";

            var param = new
            {
                id = id
            };

            return await connection.QueryFirstAsync<Product>(query, param);
        }
    }
}
