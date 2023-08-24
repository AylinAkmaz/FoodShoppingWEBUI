using FoodShoppingWEBUI.Core.Result;
using FoodShoppingWEBUI.Helper.SessionHelper;
using System.Net;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using FoodShoppingWEBUI.Core.DTO;
using FoodShoppingWEBUI.Core.ViewModel;
using NuGet.Common;

namespace FoodShoppingWEBUI.WEBUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class StoreProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StoreProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }



        [HttpGet("/Admin/MarketÜrün")]
        public async Task<IActionResult> Index()
        {
            StoreProductViewModel storeProductViewModel = new()
            {
                StoreProducts = await GetStoreProductList(),
                StoreDetails = await GetStoreDetailList()
            };

            return View(storeProductViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddStoreProduct")]
        public async Task<IActionResult> AddStoreProduct(StoreProductDTO storeProduct, IFormFile storeProductImage)
        {
            if (storeProductImage != null)
            {
                string filename = storeProductImage.FileName.Split('.')[storeProductImage.FileName.Split('.').Length - 2] + "_" + Guid.NewGuid() + "." + storeProductImage.FileName.Split('.')[storeProductImage.FileName.Split('.').Length - 1];

                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "MediaUpload", filename);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    storeProductImage.CopyTo(fileStream);
                }

                storeProduct.FeaturedImage = filename;
            }
            var url = "http://localhost:5291/AddStoreProduct";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(storeProduct);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);


            var responseObject = JsonConvert.DeserializeObject<APIResult<bool>>(restResponse.Content);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, message = responseObject.Mesaj, data = responseObject.Data });
            }
            else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, message = responseObject.Mesaj, data = responseObject.HataBilgisi });
            }

            else
            {
                return Json(new { success = false, message = "Hata Oluştu" });
            }
        }
        [HttpGet("/Admin/StoreProduct{guid}")]
        public async Task<IActionResult> GetStoreProduct(Guid guid)
        {
            var url = "http://localhost:5291/StoreProduct/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);


            var responseObject = JsonConvert.DeserializeObject<APIResult<StoreProductDTO>>(restResponse.Content);

            var storeProduct = responseObject.Data;

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

        [HttpPost("/Admin/UpdateStoreProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStoreProduct(StoreProductDTO storeProduct, IFormFile storeProductImage)
        {

            if (storeProductImage != null)
            {
                string filename = storeProductImage.FileName.Split('.')[storeProductImage.FileName.Split('.').Length - 2] + "_" + Guid.NewGuid() + "." + storeProductImage.FileName.Split('.')[storeProductImage.FileName.Split('.').Length - 1];

                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "MediaUploads", filename);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    storeProductImage.CopyTo(fileStream);
                }

                storeProduct.FeaturedImage = filename;
            }
            else
            {
                storeProduct.FeaturedImage = null;
            }
            var url = "http://localhost:5291/UpdateStoreProduct";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(storeProduct);
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

        [HttpPost("/Admin/RemoveStoreProduct/{storeProductGUID}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveStoreProduct(Guid storeProductGUID)
        {

            var url = "http://localhost:5291/RemoveStoreProduct" + storeProductGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + SessionManager.LoggedUser.Token);
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
        public async Task<List<StoreProductDTO>> GetStoreProductList()
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
        public async Task<List<StoreDetailDTO>> GetStoreDetailList()
        {
            var url = "http://localhost:5291/StoreDetail";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<StoreDetailDTO>>>(restResponse.Content);

            var storeDetails = responseObject.Data;

            return storeDetails;
        }
        public async Task<StoreDetailDTO> GetStoreDetail(Guid? guid)
        {
            var url = "http://localhost:5291/StoreDetail/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<StoreDetailDTO>>(restResponse.Content);

            var storeDetail = responseObject.Data;

            return storeDetail;
        }



    }
}
