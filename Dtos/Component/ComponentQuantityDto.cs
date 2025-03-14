using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Dtos.Component
{
    public class ComponentQuantityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public List<ComponentQuantityDto>? RequiredComponents { get; set; } = [];
    }
}
