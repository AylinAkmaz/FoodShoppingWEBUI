using FoodShoppingWEBUI.Core.DTO;
using FoodShoppingWEBUI.Core.Result;
using FoodShoppingWEBUI.Core.ViewModel;
using FoodShoppingWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]

    public class MenuController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MenuController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet("/Admin/Menu")]
        public async Task<IActionResult> Index()
        {
            MenuViewModel menuViewModel = new()
            {
                FoodDetails = await GetFoodDetailsList(),
                Menus = await GetMenusList(),
                FoodProducts = await GetFoodProductsList()
            };
            return View(menuViewModel);
        }

        [HttpGet("/Admin/Menu/{guid}")]
        public async Task<IActionResult> GetMenu(Guid guid)
        {
            var url = "http://localhost:5291/Menu/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<MenuDTO>>(restResponse.Content);

            var menu = responseObject.Data;

            return Json(new { success = true, menu });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddMenu")]
        public async Task<IActionResult> AddMenu(MenuDTO menu,IFormFile menuImage)
        {
            if (menuImage != null)
            {
                string fileName = menuImage.FileName.Split('.')[menuImage.FileName.Split('.').Length - 2] + "_" + Guid.NewGuid() + "." + menuImage.FileName.Split('.')[menuImage.FileName.Split('.').Length - 1];

                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "MediaUpload", fileName);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    menuImage.CopyTo(fileStream);
                };

                menu.FeaturedImage = fileName;
            }

            var url = "http://localhost:5291/AddMenu";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(menu);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<bool>>(restResponse.Content);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, data = responseObject.Data });
            }
            else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
            else
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }

        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/UpdateMenu")]
        public async Task<IActionResult> UpdateMenu(MenuDTO menu, IFormFile menuImage)
        {
            if (menuImage != null)
            {
                string fileName = menuImage.FileName.Split('.')[menuImage.FileName.Split('.').Length - 2] + "_" + Guid.NewGuid() + "." + menuImage.FileName.Split('.')[menuImage.FileName.Split('.').Length - 1];

                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "MediaUpload", fileName);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    menuImage.CopyTo(fileStream);
                };

                menu.FeaturedImage = fileName;
            }
            else
            {
                menu.FeaturedImage = null;
            }

            var url = "http://localhost:5291/UpdateMenu";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(menu);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<bool>>(restResponse.Content);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, data = responseObject.Data });
            }
            else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
            else
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }


        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/RemoveMenu/{menuGuid}")]

        public async Task<IActionResult> RemoveMenu(Guid menuGuid)
        {
            var url = "http://localhost:5291/RemoveMenu/" + menuGuid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<bool>>(restResponse.Content);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, data = responseObject.Data });
            }
            else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
            else
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
        }


        public async Task<List<MenuDTO>> GetMenusList()
        {
            var url = "http://localhost:5291/Menus";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<MenuDTO>>>(restResponse.Content);

            var menus = responseObject.Data;

            return menus;
        }
        public async Task<List<FoodDetailDTO>> GetFoodDetailsList()
        {
            var url = "http://localhost:5291/FoodDetails";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<FoodDetailDTO>>>(restResponse.Content);

            var foodDetails = responseObject.Data;

            return foodDetails;
        }
        public async Task<FoodDetailDTO> GetFoodDetail(Guid? guid)
        {
            var url = "http://localhost:5291/FoodDetail/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<FoodDetailDTO>>(restResponse.Content);

            var foodDetail = responseObject.Data;

            return foodDetail;
        }


        public async Task<List<FoodProductDTO>> GetFoodProductsList()
        {
            var url = "http://localhost:5291/FoodProducts";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<FoodProductDTO>>>(restResponse.Content);

            var foodProduct = responseObject.Data;

            return foodProduct;
        }
        public async Task<FoodProductDTO> GetFoodProduct(Guid? guid)
        {
            var url = "http://localhost:5291/FoodProduct/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<FoodProductDTO>>(restResponse.Content);

            var foodProduct = responseObject.Data;

            return foodProduct;
        }

    }
}
