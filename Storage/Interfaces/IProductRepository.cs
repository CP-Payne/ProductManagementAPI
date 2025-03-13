using ProductManagementAPI.Models;

namespace ProductManagementAPI.Storage.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
    }
}