using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.DTO
{
	public class StoreProductDTO
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FeaturedImage { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public Guid StoreDetailGuid { get; set; }
        public string StoreDetailName { get; set; }

    }
}
