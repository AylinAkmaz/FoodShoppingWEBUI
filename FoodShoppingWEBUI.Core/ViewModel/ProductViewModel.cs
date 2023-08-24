using FoodShoppingWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.ViewModel
{
    public class ProductViewModel
    {
        public List<ProductDTO> Product { get; set; }
        public List<FoodDetailDTO> FoodDetail { get; set; }
    }
}
