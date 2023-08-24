using FoodShoppingWEBUI.Core.DTO;
using FoodShoppingWEBUI.Core.Result;
using FoodShoppingWEBUI.Core.ViewModel;
using FoodShoppingWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]

    public class CategoryController : Controller
    {
        [HttpGet("/Admin/Kategory")]
        public async Task<IActionResult> Index()
        {
            CategoryViewModel categoryViewModel = new()
            {
                Categories = await GetCategoriesList(),
                FoodCategories = await GetFoodCategoriesList()
            };
            return View(categoryViewModel);
        }

        [HttpGet("/Admin/Category/{CategoryGuid}")]
        public async Task<IActionResult> GetCategory(Guid CategoryGuid)
        {
            var url = "http://localhost:5291/Category/" + CategoryGuid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<CategoryDTO>>(restResponse.Content);

            var category = responseObject.Data;

            return Json(new { success = true, category });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {

            var url = "http://localhost:5291/AddCategory";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(categoryDTO);
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


        public async Task<List<CategoryDTO>> GetCategoriesList()
        {
            var url = "http://localhost:5291/Categories";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<CategoryDTO>>>(restResponse.Content);

            var categories = responseObject.Data;

            return categories;
        }
        public async Task<List<FoodCategoryDTO>> GetFoodCategoriesList()
        {
            var url = "http://localhost:5291/FoodCategories";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<FoodCategoryDTO>>>(restResponse.Content);

            var foodCategories = responseObject.Data;

            return foodCategories;
        }
        public async Task<FoodCategoryDTO> GetFoodCategory(Guid? guid)
        {
            var url = "http://localhost:5291/FoodCategory/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<FoodCategoryDTO>>(restResponse.Content);

            var foodCategory = responseObject.Data;

            return foodCategory;
        }


    }
}
