using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.DTO
{
    public class StoreOrderDetailDTO
    {
        public Guid? Guid { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double? Discount { get; set; }
        public Guid StoreOrderGuid { get; set; }
        public string StoreOrderName { get; set; }
    }
}
