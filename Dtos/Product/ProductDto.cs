using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManagementAPI.Dtos.Component;

namespace ProductManagementAPI.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ComponentDto> Components { get; set; } = new List<ComponentDto>();
    }
}
