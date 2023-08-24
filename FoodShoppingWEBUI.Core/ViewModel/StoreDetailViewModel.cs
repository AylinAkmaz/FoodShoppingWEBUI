using FoodShoppingWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.ViewModel
{
    public class StoreDetailViewModel
    {
        public List<StoreDetailDTO> StoreDetails { get; set; }
        public List<StoreCategoryDTO> StoreCategories { get; set; }
    }
}
