using FoodShoppingWEBUI.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Components.StoreDetail
{
    public class StoreDetailsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<StoreDetailDTO> storeDetailDTOList)
        {
            return View("~/Areas/AdminPanel/Views/Component/StoreDetail/StoreDetails.cshtml", storeDetailDTOList);
        }
    }
}
