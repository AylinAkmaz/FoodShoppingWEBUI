using FoodShoppingWEBUI.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Components.StoreCategory
{
    public class StoreCategoriesViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<StoreCategoryDTO> storeCategoryDTOList)
        {
            return View("~/Areas/AdminPanel/Views/Component/StoreCategory/StoreCategories.cshtml", storeCategoryDTOList);
        }
    }
}
