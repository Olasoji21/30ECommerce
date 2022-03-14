using Microsoft.AspNetCore.Components;
using Shop.DataModels.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;


namespace Shop.Client.Services
{
    public class FrontEndService: IFrontEndService
    {
        private readonly HttpClient httpClient;
        private readonly ProtectedSessionStorage sessionStorage;

        public FrontEndService(HttpClient _httpClient, ProtectedSessionStorage _sessionStorage)
        {
            httpClient = _httpClient;
            sessionStorage = _sessionStorage;
        }

        public async Task<bool> IsUserLoggedIn()
        {
            bool flag = false;
            var result = await sessionStorage.GetAsync<string>("userKey");
            if(result.Success)
            {
                flag = true;
            }
            return flag;
        }


        public async Task<ResponseModel> RegisterUser(RegisterModel registerModel)
        {
            return await httpClient.PostJsonAsync<ResponseModel>("api/user/RegisterUser", registerModel);
        }

        public async Task<List<CategoryModel>> GetCategories()
        {
            return await httpClient.GetJsonAsync<List<CategoryModel>>("api/user/GetCategories");
        }

        public async Task<List<ProductModel>> GetProductByCategoryId(int categoryId)
        {
            return await httpClient.GetJsonAsync<List<ProductModel>>("api/user/GetProductByCategoryId/?categoryId=" + categoryId);
        }

        public async Task<ResponseModel> LoginUser(LoginModel loginModel)
        {
            return await httpClient.PostJsonAsync<ResponseModel>("api/user/LoginUser", loginModel);
        }


    }
}
