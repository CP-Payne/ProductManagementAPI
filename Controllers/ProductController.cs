using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManagementAPI.Service.Interfaces;

namespace ProductManagementAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManagementService _productService;

        public ProductController(IProductManagementService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var ProductList = await _productService.GetAllProductsAsync();

            return Ok(ProductList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetProductRequirementsById([FromRoute] int id)
        {
            var productWithRequirements = await _productService.GetProductWithComponentsAsync(id);
            if (productWithRequirements == null)
            {
                return NotFound();
            }

            return Ok(productWithRequirements);
        }
    }
}
