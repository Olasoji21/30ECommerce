using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.DataModels.CustomModels;
using Shop.Logic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService, IWebHostEnvironment _env)
        {
            _adminService = adminService;
            env = _env;
        }

        [HttpPost]
        [Route("AdminLogin")]
        public IActionResult AdminLogin(LoginModel loginModel)
        {
            var data = _adminService.AdminLogin(loginModel);
            return Ok(data);
        }

        [HttpPost]
        [Route("SaveCategory")]
        public IActionResult SaveCategory(CategoryModel newCategory)
        {
            var basta = _adminService.SaveCategory(newCategory);
            return Ok(basta);
        }

        [HttpGet]
        [Route("GetCategories")]
        public IActionResult GetCategories()
        {
            var Pasta = _adminService.GetCategories();
            return Ok(Pasta);
        }

        [HttpPost]
        [Route("UpdateCategory")]
        public IActionResult UpdateCategory(CategoryModel categoryToUpdate)
        {
            var Pasta = _adminService.UpdateCategory(categoryToUpdate);
            return Ok(Pasta);
        }

        [HttpPost]
        [Route("DeleteCategory")]
        public IActionResult DeleteCategory(CategoryModel categoryToDelete)
        {
            var Pasta = _adminService.DeleteCategory(categoryToDelete);
            return Ok(Pasta);
        }

        [HttpGet]
        [Route("GetProducts")]
        public IActionResult GetProducts()
        {
            var Pasta = _adminService.GetProducts();
            return Ok(Pasta);
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(ProductModel productToDelete)
        {
            var Pasta = _adminService.DeleteProduct(productToDelete);
            return Ok(Pasta);
        }

        [HttpPost]
        [Route("SaveProduct")]
        public IActionResult SaveProduct(ProductModel newProduct)
        {
            int nextProductId = _adminService.GetNewProductId();
            newProduct.ImageUrl = @"Images/" + nextProductId + ".png";
            var path = $"{env.WebRootPath}\\Images\\{nextProductId + ".png"}";
            var fs = System.IO.File.Create(path);
            fs.Write(newProduct.FIleContent, 0, newProduct.FIleContent.Length);
            fs.Close();

            string uploadsFolder = Path.Combine(env.WebRootPath, "Images");

            var Pasta = _adminService.SaveProduct(newProduct);
            return Ok(Pasta);
        }

        [HttpGet]
        [Route("GetProductStock")]
        public IActionResult GetProductStock()
        {
            var Pasta = _adminService.GetProductStock();
            return Ok(Pasta);
        }

        [HttpPost]
        [Route("UpdateProductStock")]
        public IActionResult UpdateProductStock(StockModel stock)
        {
            var Pasta = _adminService.UpdateProductStock(stock);
            return Ok(Pasta);
        }

    }
}
