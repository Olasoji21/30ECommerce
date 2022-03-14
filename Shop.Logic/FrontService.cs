using Shop.DataModels.CustomModels;
using Shop.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Logic
{
    public class FrontService : IFrontService
    {
        private readonly ECommerceDBContext db;
        public FrontService(ECommerceDBContext _db)
        {
            db = _db;
        }

        public List<CategoryModel> GetCategories()
        {
            var data = db.Categories.ToList();
            List<CategoryModel> _categoryList = new List<CategoryModel>();
            foreach (var c in data)
            {
                CategoryModel _categoryModel = new CategoryModel();
                _categoryModel.Id = c.Id;
                _categoryModel.Name = c.Name;
                _categoryList.Add(_categoryModel);
            }
            return _categoryList;
        }

        public List<ProductModel> GetProductByCategoryId(int categoryId)
        {
            var data = db.Products.Where(x => x.CategoryId == categoryId).ToList();
            List<ProductModel> _productList = new List<ProductModel>();
            foreach (var p in data)
            {
                ProductModel _productModel = new ProductModel();
                _productModel.Id = p.Id;
                _productModel.Name = p.Name;
                _productModel.Price = p.Price;
                _productModel.ImageUrl = p.ImageUrl;
                _productModel.Stock = p.Stock;
                _productList.Add(_productModel);
            }
            return _productList;
        }

        public ResponseModel RegisterUser(RegisterModel registerModel)
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                var exist_check = db.Customers.Where(x => x.Email == registerModel.EmailId).Any();
                if (!exist_check)
                {
                    Customer _customer = new Customer();
                    _customer.Name = registerModel.Name;
                    _customer.MobileNo = registerModel.MobileNo;
                    _customer.Email = registerModel.EmailId;
                    _customer.Password = registerModel.Password;
                    db.Add(_customer);
                    db.SaveChanges();

                    LoginModel loginModel = new LoginModel()
                    {
                        EmailId = registerModel.EmailId,
                        Password = registerModel.Password
                    };

                    responseModel = LoginUser(loginModel);
                }
                else
                {
                    responseModel.Status = false;
                    responseModel.Message = "Email already registered.";

                }
                return responseModel;
            }

            catch (Exception ex)
            {
                responseModel.Status = false;
                responseModel.Message = "An Error has occurred. Please try again!";
                return responseModel;
            }


        }

        public ResponseModel LoginUser(LoginModel loginModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var userData = db.Customers.Where(x => x.Email == loginModel.EmailId).FirstOrDefault();
                if (userData != null)
                {
                    if (userData.Password == loginModel.Password)
                    {
                        response.Status = true;
                        response.Message = Convert.ToString(userData.Id) + "|" + userData.Name + "|" + userData.Email;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Your password is incorrect";
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "Email not registered. Please check your Email Id";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "An error has occcurred.Please try again!";
                return response;
            }

        }
    }
}
