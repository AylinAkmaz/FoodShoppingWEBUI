using FoodShoppingWEBUI.Core.DTO;
using FoodShoppingWEBUI.Core.Result;
using FoodShoppingWEBUI.Core.ViewModel;
using FoodShoppingWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using static NuGet.Packaging.PackagingConstants;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class OrderDetailController : Controller
    {
        [HttpGet("/Admin/OrderDetail")]
        public async Task<IActionResult> Index()
        {
            OrderDetailViewModel orderDetailViewModel = new()
            {
                OrderDetails = await GetOrderDetailsList(),
                Orders = await GetOrdersList(),

            };
            return View(orderDetailViewModel);
        }

        [HttpGet("/Admin/OrderDetail/{guid}")]
        public async Task<IActionResult> GetOrderDetail(Guid guid)
        {
            var url = "http://localhost:5291/OrderDetail/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<OrderDetailDTO>>(restResponse.Content);

            var orderDetail = responseObject.Data;

            return Json(new { success = true, orderDetail });
        }
        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddOrderDetail")]
        public async Task<IActionResult> AddOrderDetail(OrderDetailDTO orderDetail)
        {

            var url = "http://localhost:5291/AddOrderDetail";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(orderDetail);
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
        [HttpPost("/Admin/UpdateOrderDetail")]
        public async Task<IActionResult> UpdateOrderDetail(OrderDetailDTO orderDetail)
        {

            var url = "http://localhost:5291/UpdateOrderDetail";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(orderDetail);
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
        [HttpPost("/Admin/RemoveOrderDetail/{orderDetailGuid}")]

        public async Task<IActionResult> RemoveOrderDetail(Guid orderDetailGuid)
        {
            var url = "http://localhost:5291/RemoveOrderDetail/" + orderDetailGuid;
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



        public async Task<List<OrderDetailDTO>> GetOrderDetailsList()
        {
            var url = "http://localhost:5291/OrderDetails";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<OrderDetailDTO>>>(restResponse.Content);

            var orderDetails = responseObject.Data;

            return orderDetails;
        }
        public async Task<List<OrderDTO>> GetOrdersList()
        {
            var url = "http://localhost:5291/Orders";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<OrderDTO>>>(restResponse.Content);

            var orders = responseObject.Data;

            return orders;
        }
        public async Task<OrderDTO> GetOrder(Guid? guid)
        {
            var url = "http://localhost:5291/Order/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<OrderDTO>>(restResponse.Content);

            var order = responseObject.Data;

            return order;
        }


    }
}
