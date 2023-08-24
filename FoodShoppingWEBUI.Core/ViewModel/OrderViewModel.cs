using FoodShoppingWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.ViewModel
{
    public class OrderViewModel
    {
        public List<FoodDetailDTO> FoodDetails { get; set; }
        public List<UserDTO> Users { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}
