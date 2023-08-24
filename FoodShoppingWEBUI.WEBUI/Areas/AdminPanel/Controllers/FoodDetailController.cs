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
    public class FoodDetailController : Controller
    {
        [HttpGet("/Admin/FoodDetail")]
        public async Task<IActionResult> Index()
        {
            FoodDetailViewModel foodDetailViewModel = new()
            {
                FoodDetails = await GetFoodDetailsList(),
                Roles = await GetRolesList(),
                FoodCategories = await GetFoodCategoriesList()
            };
            return View(foodDetailViewModel);
        }

        [HttpGet("/Admin/FoodDetail/{guid}")]
        public async Task<IActionResult> GetFoodDetail(Guid guid)
        {
            var url = "http://localhost:5291/FoodDetail/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<FoodDetailDTO>>(restResponse.Content);

            var foodDetail = responseObject.Data;

            return Json(new { success = true, foodDetail });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddFoodDetail")]
        public async Task<IActionResult> AddFoodDetail(FoodDetailDTO foodDetailDTO)
        {

            var url = "http://localhost:5291/AddFoodDetail";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(foodDetailDTO);
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
        [HttpPost("/Admin/UpdateFoodDetail")]
        public async Task<IActionResult> UpdateFoodDetail(FoodDetailDTO foodDetailDTO)
        {

            var url = "http://localhost:5291/UpdateFoodDetail";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(foodDetailDTO);
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
        [HttpPost("/Admin/RemoveFoodDetail/{foodDetailGuid}")]

        public async Task<IActionResult> RemoveFoodDetail(Guid foodDetailGuid)
        {
            var url = "http://localhost:5291/RemoveFoodDetail/" + foodDetailGuid;
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
        public async Task<FoodCategoryDTO> GetStoreCategory(Guid? guid)
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


        public async Task<List<RoleDTO>> GetRolesList()
        {
            var url = "http://localhost:5291/Rols";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<RoleDTO>>>(restResponse.Content);

            var roles = responseObject.Data;

            return roles;
        }
        public async Task<RoleDTO> GetRole(Guid? guid)
        {
            var url = "http://localhost:5291/Role/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<RoleDTO>>(restResponse.Content);

            var role = responseObject.Data;

            return role;
        }

    }
}
