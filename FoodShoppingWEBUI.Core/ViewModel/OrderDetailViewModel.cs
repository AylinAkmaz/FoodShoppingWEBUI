using FoodShoppingWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.ViewModel
{
    public class OrderDetailViewModel
    {
        public List<OrderDTO> Orders { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }

    }
}
