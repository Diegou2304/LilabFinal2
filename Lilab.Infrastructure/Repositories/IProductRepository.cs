using Lilab.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilab.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task<int> UpdateProduct(Product product);
        Task<Product> GetProductById(int id);
    }
}
