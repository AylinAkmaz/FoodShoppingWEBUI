using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.DTO
{
    public class StoreOrderDTO
    {
        public Guid? Guid { get; set; }
        public string Name { get; set; }
        public Guid UserGuid { get; set; }
        public Guid StoreProductGuid { get; set; }
        public string StoreProductName { get; set; }
        public string UserName { get; set; }

    }
}
