using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.DTO
{
    public class StoresDTO
    {
        public string Name { get; set; }
        public Guid StoreCategoryGuid { get; set; }
        public string StoreCategoryName { get; set; }
        public Guid? Guid { get; set; }

    }
}
