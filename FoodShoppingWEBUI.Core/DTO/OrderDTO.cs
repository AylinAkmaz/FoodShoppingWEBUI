using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.DTO
{
	public class OrderDTO
    {
        public Guid? Guid { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string FoodDetailName { get; set; }
        public Guid UserGuid { get; set; }
        public Guid FoodDetailGuid { get; set; }
    }
}
