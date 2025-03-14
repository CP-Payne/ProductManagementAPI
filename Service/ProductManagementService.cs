using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProductManagementAPI.Dtos;
using ProductManagementAPI.Dtos.Component;
using ProductManagementAPI.Dtos.Product;
using ProductManagementAPI.Models;
using ProductManagementAPI.Service.Interfaces;
using ProductManagementAPI.Storage;
using ProductManagementAPI.Storage.Repository;

namespace ProductManagementAPI.Service
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDBContext _context;

        public ProductManagementService(
            IProductRepository productRepo,
            ApplicationDBContext context
        )
        {
            _productRepository = productRepo;
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<ProductDto> GetProductWithComponentsAsync(int productId)
        {
            // Step 1: Load the product with its direct components
            var product = await _context.Products
                .Include(p => p.ProductComponents)
                    .ThenInclude(pc => pc.component)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                return null;

            // Step 2: Load all components in a separate query
            var allComponents = await _context.Components.AsNoTracking().ToListAsync();

            // Step 3: Load all component relationships in a separate query
            var allComponentRelationships = await _context.Set<ComponentComponent>().AsNoTracking().ToListAsync();

            // Create the product DTO
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Components = new List<ComponentDto>()
            };

            // Add top-level components
            foreach (var pc in product.ProductComponents)
            {
                var componentDto = new ComponentDto
                {
                    Id = pc.ComponentId,
                    Name = pc.component.Name,
                    Quantity = pc.Quantity,
                    SubComponents = new List<ComponentDto>()
                };

                // Build the component hierarchy manually
                BuildComponentHierarchy(componentDto, allComponents, allComponentRelationships, new HashSet<int>());

                productDto.Components.Add(componentDto);
            }

            return productDto;
        }

        private void BuildComponentHierarchy(
            ComponentDto parentDto,
            List<Component> allComponents,
            List<ComponentComponent> relationships,
            HashSet<int> processedIds)
        {
            // Avoid processing the same component twice (prevents cycles)
            if (processedIds.Contains(parentDto.Id))
                return;

            processedIds.Add(parentDto.Id);

            // Find all child relationships for this component
            var childRelationships = relationships
                .Where(r => r.ParentComponentId == parentDto.Id)
                .ToList();

            foreach (var relation in childRelationships)
            {
                // Find the component details
                var childComponent = allComponents.FirstOrDefault(c => c.Id == relation.ChildComponentId);
                if (childComponent == null) continue;

                var childDto = new ComponentDto
                {
                    Id = childComponent.Id,
                    Name = childComponent.Name,
                    Quantity = relation.Quantity,
                    SubComponents = new List<ComponentDto>()
                };

                // Create a new set for tracking processed components in this branch
                var newProcessedIds = new HashSet<int>(processedIds);

                // Recursively build child components
                BuildComponentHierarchy(childDto, allComponents, relationships, newProcessedIds);

                parentDto.SubComponents.Add(childDto);
            }
        }
    }
}

