using FoodShoppingWEBUI.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Components.FoodCategory
{
    public class FoodCategoriesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<FoodCategoryDTO> foodCategoryDTOList)
        {
            return View("~/Areas/AdminPanel/Views/Component/FoodCategory/FoodCategories.cshtml", foodCategoryDTOList);
        }
    }
}
