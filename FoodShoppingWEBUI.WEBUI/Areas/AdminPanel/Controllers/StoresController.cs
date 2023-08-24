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

    public class StoresController : Controller
    {
        [HttpGet("/Admin/Stores")]
        public async Task<IActionResult> Index()
        {
            StoresViewModel storesViewModel = new()
            {
                Stores = await GetStoresList(),
                StoreCategories = await GetStoreCategoriesList()
            };
            return View(storesViewModel);
        }

        [HttpGet("/Admin/Stores/{StoresGuid}")]
        public async Task<IActionResult> GetStores(Guid StoresGuid)
        {
            var url = "http://localhost:5291/Stores/" + StoresGuid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<StoresDTO>>(restResponse.Content);

            var stores = responseObject.Data;

            return Json(new { success = true, stores });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddStores")]
        public async Task<IActionResult> AddStores(StoresDTO storesDTO)
        {
            
                var url = "http://localhost:5291/AddStores";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
                var body = JsonConvert.SerializeObject(storesDTO);
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


        public async Task<List<StoresDTO>> GetStoresList()
        {
            var url = "http://localhost:5291/Stores";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<StoresDTO>>>(restResponse.Content);

            var stores = responseObject.Data;

            return stores;
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
        public async Task<StoreCategoryDTO> GetStoreCategories(Guid? guid)
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
