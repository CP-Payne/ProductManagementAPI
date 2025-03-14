using ProductManagementAPI.Models;

namespace ProductManagementAPI.Storage.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductWithRequirementsByIdAsync(int id);
    }
}