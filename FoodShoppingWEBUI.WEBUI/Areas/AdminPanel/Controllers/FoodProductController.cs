using FoodShoppingWEBUI.Core.DTO;
using FoodShoppingWEBUI.Core.Result;
using FoodShoppingWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class FoodProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FoodProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet("/Admin/YemekÜrünleri")]
        public async Task<IActionResult> Index()
        {
            var url = "http://localhost:5291/FoodProducts";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<FoodProductDTO>>>(restResponse.Content);

            var foodProducts = responseObject.Data;

            return View(foodProducts);
        }

        [HttpPost("/Admin/AddFoodProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFoodProduct(FoodProductDTO foodProduct, IFormFile foodProductImage)
        {
            if (foodProductImage != null)
            {
                string fileName = foodProductImage.FileName.Split('.')[foodProductImage.FileName.Split('.').Length - 2] + "_" + Guid.NewGuid() + "." + foodProductImage.FileName.Split('.')[foodProductImage.FileName.Split('.').Length - 1];

                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "MediaUpload", fileName);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    foodProductImage.CopyTo(fileStream);
                };

                foodProduct.FeaturedImage = fileName;


                var url = "http://localhost:5291/AddFoodProduct";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
                var body = JsonConvert.SerializeObject(foodProduct);
                request.AddBody(body, "application/json");
                RestResponse restResponse = await client.ExecuteAsync(request);

                var responseObject = JsonConvert.DeserializeObject<APIResult<FoodProductDTO>>(restResponse.Content);

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
            else
            {
                return Json(new { });
            }
        }
        [HttpGet("/Admin/FoodProduct/{guid}")]
        public async Task<IActionResult> GetFoodProduct(Guid guid)
        {
            var url = "http://localhost:5291/FoodProduct/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<FoodProductDTO>>(restResponse.Content);

            var foodProduct = responseObject.Data;

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

        [HttpPost("/Admin/UpdateFoodProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFoodProduct(FoodProductDTO foodProduct, IFormFile foodProductImage)
        {
            if (foodProductImage != null)
            {
                string fileName = foodProductImage.FileName.Split('.')[foodProductImage.FileName.Split('.').Length - 2] + "_" + Guid.NewGuid() + "." + foodProductImage.FileName.Split('.')[foodProductImage.FileName.Split('.').Length - 1];

                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "MediaUpload", fileName);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    foodProductImage.CopyTo(fileStream);
                };

                foodProduct.FeaturedImage = fileName;



                var url = "http://localhost:5291/UpdateFoodProduct";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
                var body = JsonConvert.SerializeObject(foodProduct);
                request.AddBody(body, "application/json");
                RestResponse restResponse = await client.ExecuteAsync(request);

                var responseObject = JsonConvert.DeserializeObject<APIResult<FoodProductDTO>>(restResponse.Content);

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
            else
            {
                return Json(new { });
            }
        }


        [HttpPost("/Admin/RemoveFoodProduct/{FoodProductGuid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFoodProduct(Guid FoodProductGuid)
        {
            var url = "http://localhost:5291/RemoveFoodProduct/" + FoodProductGuid;
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

