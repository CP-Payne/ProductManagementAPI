using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Models
{
    public class ProductComponent
    {
        public int ProductId { get; set; }
        public int ComponentId { get; set; }
        public int Quantity { get; set; }

        public Product product = null!;
        public Component component = null!;
    }
}