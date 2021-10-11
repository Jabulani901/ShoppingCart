using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _config;
        public ProductController(IConfiguration config, IProductService productService)
        {
            _config = config;
            _productService = productService;
        }
        [HttpGet("GetAllProduct")]
        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await this._productService.GetProduct();
        }
        [HttpPost("BuyProduct")]
        public async Task AddNewUser([FromBody] Product product)
        {
            await this._productService.AddProduct(product);
        }
    }
}
