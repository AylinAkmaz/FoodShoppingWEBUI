using FoodShoppingWEBUI.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Components.Product
{
    public class ProductsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<ProductDTO> productDTOList)
        {
            return View("~/Areas/AdminPanel/Views/Component/Product/Products.cshtml", productDTOList);
        }
    }
}
