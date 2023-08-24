using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.DTO
{
	public class OrderDetailDTO
    {
        public string Name { get; set; }
        public string OrderName { get; set; }
        public Guid OrderGuid { get; set; }
        public Guid Guid { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double? Discount { get; set; }
    }
}
