using FoodShoppingWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.ViewModel
{
    public class StoreProductViewModel
    {
        public List<StoreProductDTO> StoreProducts { get; set; }
        public List<StoreDetailDTO> StoreDetails { get; set; }
    }
}
