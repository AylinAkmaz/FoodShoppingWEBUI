using FoodShoppingWEBUI.Core.Result;
using FoodShoppingWEBUI.Helper.SessionHelper;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using FoodShoppingWEBUI.Core.DTO;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Controllers
{
	[Area("AdminPanel")]
	public class UserController : Controller
	{
		[HttpGet("/Admin/Kullanıcılar")]
		public async Task<IActionResult> Index()
		{
			var url = "http://localhost:5291/Users";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Get);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
			RestResponse restResponse = await client.ExecuteAsync(request);

			var responseObject = JsonConvert.DeserializeObject<APIResult<List<UserDTO>>>(restResponse.Content);

			var users = responseObject.Data;

			return View(users);
		}
        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddUser")]
        public async Task<IActionResult> AddUser(UserDTO user)
        {

            var url = "http://localhost:5291/AddUser";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(user);
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

        [HttpGet("/Admin/User/{userGUID}")]
		public async Task<IActionResult> GetUser(Guid userGUID)
		{
			var url = "http://localhost:5291/User/" + userGUID;
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Get);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
			RestResponse restResponse = await client.ExecuteAsync(request);

			var responseObject = JsonConvert.DeserializeObject<APIResult<UserDTO>>(restResponse.Content);

			var category = responseObject.Data;

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
