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
    public class StoreDetailController : Controller
    {
        [HttpGet("/Admin/StoreDetail")]
        public async Task<IActionResult> Index()
        {
            StoreDetailViewModel storeDetailViewModel = new()
            {
                StoreDetails = await GetStoreDetailList(),
                StoreCategories = await GetStoreCategoriesList()
            };
            return View(storeDetailViewModel);
        }

        [HttpGet("/Admin/StoreDetail/{guid}")]
        public async Task<IActionResult> GetStoreDetail(Guid guid)
        {
            var url = "http://localhost:5291/StoreDetail/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<StoreDetailDTO>>(restResponse.Content);

            var storeDetail = responseObject.Data;

            return Json(new { success = true, storeDetail });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddStoreDetail")]
        public async Task<IActionResult> AddStoreDetail(StoreDetailDTO storeDetailDTO)
        {

            var url = "http://localhost:5291/AddStoreDetail";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(storeDetailDTO);
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
        [HttpPost("/Admin/UpdateStoreDetail")]
        public async Task<IActionResult> UpdateStoreDetail(StoreDetailDTO storeDetailDTO)
        {

            var url = "http://localhost:5291/UpdateStoreDetail";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(storeDetailDTO);
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
        [HttpPost("/Admin/RemoveStoreDetail/{storeDetailGuid}")]

        public async Task<IActionResult> RemoveStoreDetail(Guid storeDetailGuid)
        {
            var url = "http://localhost:5291/RemoveStoreDetail/" + storeDetailGuid;
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



        public async Task<List<StoreDetailDTO>> GetStoreDetailList()
        {
            var url = "http://localhost:5291/StoreDetail";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<StoreDetailDTO>>>(restResponse.Content);

            var storeDetail = responseObject.Data;

            return storeDetail;
        }
        public async Task<List<StoreCategoryDTO>> GetStoreCategoriesList()
        {
            var url = "http://localhost:5291/StoreCategories";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<StoreCategoryDTO>>>(restResponse.Content);

            var storeCategories = responseObject.Data;

            return storeCategories;
        }
        public async Task<StoreCategoryDTO> GetStoreCategory(Guid? guid)
        {
            var url = "http://localhost:5291/StoreCategory/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<StoreCategoryDTO>>(restResponse.Content);

            var storeCategories = responseObject.Data;

            return storeCategories;
        }


    }
}
