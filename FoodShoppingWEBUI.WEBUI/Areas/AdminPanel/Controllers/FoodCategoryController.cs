using FoodShoppingWEBUI.Core.DTO;
using FoodShoppingWEBUI.Core.Result;
using FoodShoppingWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class FoodCategoryController : Controller
    {
        [HttpGet("/Admin/FoodCategories")]
        public async Task<IActionResult> Index()
        {
            var url = "http://localhost:5291/FoodCategories";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<FoodCategoryDTO>>>(restResponse.Content);

            var foodCategories = responseObject.Data;

            return View(foodCategories);
        }

        [HttpPost("/Admin/AddFoodCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFoodCategory(FoodCategoryDTO foodCategory)
        {
            var url = "http://localhost:5291/AddFoodCategory";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(foodCategory);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<FoodCategoryDTO>>(restResponse.Content);

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

        [HttpGet("/Admin/FoodCategory/{guid}")]
        public async Task<IActionResult> GetFoodCategory(Guid guid)
        {
            var url = "http://localhost:5291/FoodCategory/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<FoodCategoryDTO>>(restResponse.Content);

            var foodCategory = responseObject.Data;

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

        [HttpPost("/Admin/UpdateFoodCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFoodCategory(FoodCategoryDTO foodCategory)
        {
            var url = "http://localhost:5291/UpdateFoodCategory";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(foodCategory);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<FoodCategoryDTO>>(restResponse.Content);

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


        [HttpPost("/Admin/RemoveFoodCategory/{FoodCategoryGuid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFoodCategory(Guid FoodCategoryGuid)
        {
            var url = "http://localhost:5291/RemoveFoodCategory/" + FoodCategoryGuid;
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
    }
}
