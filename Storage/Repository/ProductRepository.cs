using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Models;
using ProductManagementAPI.Storage;

namespace ProductManagementAPI.Storage.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductWithRequirementsByIdAsync(int id)
        {
            // return await _context
            //     .Products.Include(p => p.ProductComponents)
            //     .ThenInclude(pc => pc.component)
            //     .Include(p => p.ProductComponents)
            //     .ThenInclude(pc => pc.component.ChildComponents)
            //     // .ThenInclude(cc => cc.ChildComponent)
            //     .FirstOrDefaultAsync(p => p.Id == id);

            var product = await _context.Products
       .Include(p => p.ProductComponents)
           .ThenInclude(pc => pc.component)
       .FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                foreach (var pc in product.ProductComponents)
                {
                    if (pc.component != null)
                    {
                        await LoadChildComponentsRecursively(pc.component);
                    }
                }
            }

            return product;

        }

        private async Task LoadChildComponentsRecursively(Component component)
        {
            await _context.Entry(component)
                .Collection(c => c.ChildComponents)
                .Query()
                .Include(cc => cc.ChildComponent)
                .LoadAsync();

            foreach (var childComponent in component.ChildComponents.Select(cc => cc.ChildComponent))
            {
                if (childComponent != null)
                {
                    await LoadChildComponentsRecursively(childComponent);
                }
            }
        }
    }
}
