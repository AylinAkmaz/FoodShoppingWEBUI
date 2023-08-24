using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShoppingWEBUI.Core.DTO
{
	public class FoodDetailDTO
    {
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public string Adress { get; set; }
        public string DeliveryTıme { get; set; }
        public string Openning { get; set; }
        public string Closing { get; set; }
        public string About { get; set; }
        public Guid FoodCategoryGuid { get; set; }
        public Guid RoleGuid { get; set; }
        public string FoodCategoryName { get; set; }
        public string RoleName { get; set; }

    }
}
