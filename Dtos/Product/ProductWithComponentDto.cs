using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManagementAPI.Dtos.Component;

namespace ProductManagementAPI.Dtos.Product
{
    public class ProductWithComponentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<ComponentQuantityDto> RequiredComponents { get; set; } = [];
    }
}