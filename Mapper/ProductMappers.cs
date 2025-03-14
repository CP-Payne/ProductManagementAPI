using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManagementAPI.Dtos;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Mapper
{
    public static class ProductMappers
    {
        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto { Id = productModel.Id, Name = productModel.Name };
        }
    }
}
