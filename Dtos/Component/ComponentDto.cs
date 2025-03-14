using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Dtos.Component
{
    public class ComponentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public List<ComponentDto> SubComponents { get; set; } = new List<ComponentDto>();
    }
}