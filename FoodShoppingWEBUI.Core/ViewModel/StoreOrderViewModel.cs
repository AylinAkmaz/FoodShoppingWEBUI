using FoodShoppingWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.ViewModel
{
    public class StoreOrderViewModel
    {
        public List<StoreProductDTO> StoreProducts { get; set; }
        public List<UserDTO> Users { get; set; }
        public List<StoreOrderDTO> StoreOrders { get; set; }
    }
}
