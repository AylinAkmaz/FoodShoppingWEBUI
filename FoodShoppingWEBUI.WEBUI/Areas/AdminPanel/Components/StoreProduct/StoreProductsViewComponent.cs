using FoodShoppingWEBUI.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Components.StoreProduct
{
    public class StoreProductsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<StoreProductDTO> storeProductDTO)
        {
            return View("~/Areas/AdminPanel/Views/Component/StoreProduct/StoreProducts.cshtml", storeProductDTO);
        }
    }
}
