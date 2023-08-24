using FoodShoppingWEBUI.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Components.FoodProduct
{
    public class FoodProductViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<FoodProductDTO> foodProductDTOList)
        {
            return View("~/Areas/AdminPanel/Views/Component/FoodProduct/FoodProducts.cshtml", foodProductDTOList);
        }
    }
}
