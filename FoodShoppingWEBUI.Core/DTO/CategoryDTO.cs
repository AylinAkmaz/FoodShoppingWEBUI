using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.DTO
{
	public class CategoryDTO
    {
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public Guid FoodCategoryGuid { get; set; }
        public string FoodCategoryName { get; set; }

    }
}
