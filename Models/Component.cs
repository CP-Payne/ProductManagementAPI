using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Models
{
    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ProductComponent> ProductComponents { get; set; } = [];

        public List<ComponentComponent> ChildComponents { get; set; } = [];
        public List<ComponentComponent> ParentComponents { get; set; } = [];
    }
}
