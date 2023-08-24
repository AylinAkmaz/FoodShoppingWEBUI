using FoodShoppingWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.ViewModel
{
    public class StoresViewModel
    {
        public List<StoresDTO> Stores { get; set; }
        public List<StoreCategoryDTO> StoreCategories { get; set; }
    }
}
