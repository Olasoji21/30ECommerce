using Shop.DataModels.CustomModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Logic
{
   public interface IFrontService
    {
        List<CategoryModel> GetCategories();

        List<ProductModel> GetProductByCategoryId(int categoryId);

        ResponseModel RegisterUser(RegisterModel registerModel);

        ResponseModel LoginUser(LoginModel loginModel);


    }
}
