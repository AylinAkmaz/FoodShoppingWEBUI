using FoodShoppingWEBUI.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Components.Role
{
    public class RoleViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<RoleDTO> roleDTOList)
        {
            return View("~/Areas/AdminPanel/Views/Component/Role/Rols.cshtml", roleDTOList);
        }
    }
}
