using FoodShoppingWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.ViewModel
{
    public class CategoryViewModel
    {
        public List<CategoryDTO> Categories { get; set; }
        public List<FoodCategoryDTO> FoodCategories { get; set; }
    }
}
