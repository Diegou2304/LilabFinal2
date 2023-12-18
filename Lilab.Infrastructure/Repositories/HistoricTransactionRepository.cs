using Lilab.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Dapper;

namespace Lilab.Infrastructure.Repositories
{
    public class HistoricTransactionRepository : IHistoricTransactionRepository
    {
        private readonly string _connectionString;

        public HistoricTransactionRepository(IOptions<ConnectionStringConfig> connectionString)
        {
            _connectionString = connectionString.Value.Value;
        }


        public async Task<int> InsertProductHistoric(Product product)
        {
            using var connection = new SqlConnection(_connectionString);

            var result = await InsertHistoric(product.Stock);

            if (result > 0)
            {
                var query = @"Insert into ProductHistoric values (@productId, @HistoricId)";

                var param = new
                {
                    productId = product.Id,
                    HistoricId = await GetLastInsertedHistoricId()
                };

                return await connection.ExecuteAsync(query, param);
            }
            return (0);

        }



        private async Task<int> InsertHistoric(int stock)
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"Insert into Historic values (@date,@stock)";

            var param = new
            {
                stock = stock,
                date = DateTime.Now
            };

            return await connection.ExecuteAsync(query, param);

        }

        private async Task<int> GetLastInsertedHistoricId()
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"select TOP 1 id from Historic order by ID desc";



            return await connection.QueryFirstAsync<int>(query);

        }
    }
}

