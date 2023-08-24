using FoodShoppingWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.ViewModel
{
    public class StoreOrderDetailViewModel
    {
        public List<StoreOrderDTO> StoreOrders { get; set; }
        public List<StoreOrderDetailDTO> StoreOrderDetails { get; set; }
    }
}
