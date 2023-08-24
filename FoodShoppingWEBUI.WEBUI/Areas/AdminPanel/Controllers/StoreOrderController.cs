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
    public class StoreOrderController : Controller
    {
        [HttpGet("/Admin/StoreOrder")]
        public async Task<IActionResult> Index()
        {
            StoreOrderViewModel storeOrderViewModel = new()
            {
                Users = await GetUsersList(),
                StoreOrders = await GetStoreOrdersList(),
                StoreProducts = await GetStoreProductsList()
            };
            return View(storeOrderViewModel);
        }

        [HttpGet("/Admin/StoreOrder/{guid}")]
        public async Task<IActionResult> GetStoreOrder(Guid guid)
        {
            var url = "http://localhost:5291/StoreOrder/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<StoreOrderDTO>>(restResponse.Content);

            var storeOrder = responseObject.Data;

            return Json(new { success = true, storeOrder });
        }
        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddStoreOrder")]
        public async Task<IActionResult> AddStoreOrder(StoreOrderDTO storeOrder)
        {

            var url = "http://localhost:5291/AddStoreOrder";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(storeOrder);
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
        [HttpPost("/Admin/UpdateStoreOrder")]
        public async Task<IActionResult> UpdateStoreOrder(StoreOrderDTO storeOrder)
        {

            var url = "http://localhost:5291/UpdateStoreOrder";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(storeOrder);
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
        [HttpPost("/Admin/RemoveStoreOrder/{storeOrderGuid}")]

        public async Task<IActionResult> RemoveStoreOrder(Guid storeOrderGuid)
        {
            var url = "http://localhost:5291/RemoveStoreOrder/" + storeOrderGuid;
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



        public async Task<List<StoreOrderDTO>> GetStoreOrdersList()
        {
            var url = "http://localhost:5291/StoreOrders";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<StoreOrderDTO>>>(restResponse.Content);

            var storeOrders = responseObject.Data;

            return storeOrders;
        }
        public async Task<List<UserDTO>> GetUsersList()
        {
            var url = "http://localhost:5291/Users";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<UserDTO>>>(restResponse.Content);

            var users = responseObject.Data;

            return users;
        }
        public async Task<UserDTO> GetUser(Guid? guid)
        {
            var url = "http://localhost:5291/User/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<UserDTO>>(restResponse.Content);

            var user = responseObject.Data;

            return user;
        }


        public async Task<List<StoreProductDTO>> GetStoreProductsList()
        {
            var url = "http://localhost:5291/StoreProducts";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<StoreProductDTO>>>(restResponse.Content);

            var storeProducts = responseObject.Data;

            return storeProducts;
        }
        public async Task<StoreProductDTO> GetStoreProduct(Guid? guid)
        {
            var url = "http://localhost:5291/StoreProduct/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<StoreProductDTO>>(restResponse.Content);

            var storeProduct = responseObject.Data;

            return storeProduct;
        }
    }
}
