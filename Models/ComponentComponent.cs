using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Models
{
    public class ComponentComponent
    {
        public int ParentComponentId { get; set; }
        public Component ParentComponent { get; set; } = null!;

        public int ChildComponentId { get; set; }
        public Component ChildComponent { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
