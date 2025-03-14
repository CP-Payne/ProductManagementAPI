using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManagementAPI.Dtos;
using ProductManagementAPI.Dtos.Product;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Service.Interfaces
{
    public interface IProductManagementService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<ProductDto> GetProductWithComponentsAsync(int id);
    }
}
