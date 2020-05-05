using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Product;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// Adds a new product into the Database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("api/product")]
        public ActionResult AddProduct([FromBody] ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _logger.LogInformation("Adding product");
            var newProduct = ProductMapper.SerializeProductViewModel(product);
            var newProductResponse = _productService.CreateProduct(newProduct);

            return Ok(newProductResponse);
        }
            
           
        /// <summary>
        /// returns all the products
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/product")]
        public ActionResult GetProduct()
        {
            _logger.LogInformation("Getting all products");
            var products = _productService.GetAllProducts();
            var productViewModels = products.Select(ProductMapper.SerializeProductViewModel);
            //var productViewModels = products.Select(product => ProductMapper.SerializeProductViewModel(product));
            return Ok(productViewModels);
        }

        /// <summary>
        /// Archives an existing product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("/api/product/{id}")]
        public ActionResult ArchiveProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _logger.LogInformation("Archiving product");
            var archiveResult = _productService.ArchivedProduct(id);
            return Ok(archiveResult);
        }
    }
}