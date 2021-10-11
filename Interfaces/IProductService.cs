using ShoppingCart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart.Interfaces
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task<IEnumerable<Product>> GetProduct();
    }
}
