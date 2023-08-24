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
    public class StoreOrderDetailController : Controller
    {
        [HttpGet("/Admin/StoreOrderDetail")]
        public async Task<IActionResult> Index()
        {
            StoreOrderDetailViewModel storeOrderDetailViewModel = new()
            {
                StoreOrderDetails = await GetStoreOrderDetailsList(),
                StoreOrders = await GetStoreOrdersList(),
                
            };
            return View(storeOrderDetailViewModel);
        }

        [HttpGet("/Admin/StoreOrderDetail/{guid}")]
        public async Task<IActionResult> GetStoreOrderDetail(Guid guid)
        {
            var url = "http://localhost:5291/StoreOrderDetail/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<StoreOrderDetailDTO>>(restResponse.Content);

            var storeOrderDetail = responseObject.Data;

            return Json(new { success = true, storeOrderDetail });
        }
        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddStoreOrderDetail")]
        public async Task<IActionResult> AddStoreOrderDetail(StoreOrderDetailDTO storeOrderDetail)
        {

            var url = "http://localhost:5291/AddStoreOrderDetail";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(storeOrderDetail);
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
        [HttpPost("/Admin/UpdateStoreOrderDetail")]
        public async Task<IActionResult> UpdateStoreOrderDetail(StoreOrderDetailDTO storeOrderDetail)
        {

            var url = "http://localhost:5291/UpdateStoreOrderDetail";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(storeOrderDetail);
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
        [HttpPost("/Admin/RemoveStoreOrderDetail/{storeOrderDetailGuid}")]

        public async Task<IActionResult> RemoveStoreOrderDetail(Guid storeOrderDetailGuid)
        {
            var url = "http://localhost:5291/RemoveStoreOrderDetail/" + storeOrderDetailGuid;
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



        public async Task<List<StoreOrderDetailDTO>> GetStoreOrderDetailsList()
        {
            var url = "http://localhost:5291/StoreOrderDetails";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<StoreOrderDetailDTO>>>(restResponse.Content);

            var storeOrderDetails = responseObject.Data;

            return storeOrderDetails;
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
        public async Task<StoreOrderDTO> GetStoreOrder(Guid? guid)
        {
            var url = "http://localhost:5291/StoreOrder/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<StoreOrderDTO>>(restResponse.Content);

            var storeOrder = responseObject.Data;

            return storeOrder;
        }


    }
}
