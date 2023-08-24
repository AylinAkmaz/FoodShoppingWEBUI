using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.DTO
{
	public class MenuDTO
	{
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public Guid FoodProductGuid { get; set; }
        public Guid FoodDetailGuid { get; set; }
        public string FoodProductName { get; set; }
        public string FoodDetailName { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public string Description { get; set; }
        public string FeaturedImage { get; set; }
    }
}
