using Shop.DataModels.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Client.Services
{
   public interface IFrontEndService
    {
        Task<List<CategoryModel>> GetCategories();

        Task<List<ProductModel>> GetProductByCategoryId(int categoryId);

        Task<bool> IsUserLoggedIn();

        Task<ResponseModel> RegisterUser(RegisterModel registerModel);

        Task<ResponseModel> LoginUser(LoginModel loginModel);
    }
}
