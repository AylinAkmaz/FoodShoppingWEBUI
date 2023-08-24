using FoodShoppingWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.ViewModel
{
    public class MenuViewModel
    {
        public List<MenuDTO> Menus { get; set; }
        public List<FoodProductDTO> FoodProducts { get; set; }
        public List<FoodDetailDTO> FoodDetails { get; set; }

    }
}
