using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Logic;
using Shop.DataModels.CustomModels;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly IFrontService frontService;

        public UserController(IWebHostEnvironment _env, IFrontService _frontService)
        {
            env = _env;
            frontService = _frontService;
        }

        [HttpGet]
        [Route("GetCategories")]
        public IActionResult GetCategories()
        {
            var data = frontService.GetCategories();
            return Ok(data);
        }

        [HttpGet]
        [Route("GetProductByCategoryId")]
        public IActionResult GetProductByCategoryId(int categoryId)
        {
            var data = frontService.GetProductByCategoryId(categoryId);
            return Ok(data);
        }

        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser(RegisterModel registerModel)
        {
            var data = frontService.RegisterUser(registerModel);
            return Ok(data);
        }

        [HttpPost]
        [Route("LoginUser")]
        public IActionResult AdminLogin(LoginModel loginModel)
        {
            var data = frontService.LoginUser(loginModel);
            return Ok(data);
        }

    }
}
