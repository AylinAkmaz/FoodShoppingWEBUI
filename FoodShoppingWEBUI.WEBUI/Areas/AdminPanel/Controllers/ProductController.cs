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
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("/Admin/Product")]
        public async Task<IActionResult> Index()
        {
            ProductViewModel productViewModel = new()
            {
                Product = await GetProductList(),
                FoodDetail = await GetFoodDetailList()
            };
            return View(productViewModel);
        }

        [HttpGet("/Admin/Product/{guid}")]
        public async Task<IActionResult> GetProduct(Guid guid)
        {
            var url = "http://localhost:5291/Product/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<ProductDTO>>(restResponse.Content);

            var product = responseObject.Data;

            return Json(new { success = true, product });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddProduct")]
        public async Task<IActionResult> AddProduct(ProductDTO productDTO, IFormFile productImage)
        {
            if (productImage != null)
            {
                string fileName = productImage.FileName.Split('.')[productImage.FileName.Split('.').Length - 2] + "_" + Guid.NewGuid() + "." + productImage.FileName.Split('.')[productImage.FileName.Split('.').Length - 1];

                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "MediaUpload", fileName);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    productImage.CopyTo(fileStream);
                };

                productDTO.FeaturedImage = fileName;

                var url = "http://localhost:5291/AddProduct";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
                var body = JsonConvert.SerializeObject(productDTO);
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
            else
            {
                return Json(new { });
            }

        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductDTO productDTO, IFormFile productImage)
        {

            if (productImage != null)
            {
                string fileName = productImage.FileName.Split('.')[productImage.FileName.Split('.').Length - 2] + "_" + Guid.NewGuid() + "." + productImage.FileName.Split('.')[productImage.FileName.Split('.').Length - 1];

                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "MediaUpload", fileName);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    productImage.CopyTo(fileStream);
                };

                productDTO.FeaturedImage = fileName;
            }

            var url = "http://localhost:5291/UpdateProduct";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(productDTO);
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
        [HttpPost("/Admin/RemoveProduct/{productGuid}")]

        public async Task<IActionResult> RemoveProduct(Guid productGuid)
        {
            var url = "http://localhost:5291/RemoveProduct/" + productGuid;
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

        public async Task<List<ProductDTO>> GetProductList()
        {
            var url = "http://localhost:5291/Products";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<List<ProductDTO>>>(restResponse.Content);

            var products = responseObject.Data;

            return products;
        }
        public async Task<List<FoodDetailDTO>> GetFoodDetailList()
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
        public async Task<FoodDetailDTO> GetFoodDetail(Guid? guid)
        {
            var url = "http://localhost:5291/FoodDetail/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<APIResult<FoodDetailDTO>>(restResponse.Content);

            var foodDetail = responseObject.Data;

            return foodDetail;
        }

    }
}

