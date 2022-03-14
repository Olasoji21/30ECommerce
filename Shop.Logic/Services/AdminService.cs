using Shop.DataModels.CustomModels;
using Shop.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Logic.Services
{
    public class AdminService : IAdminService
    {
        private readonly ECommerceDBContext _db;
        public AdminService(ECommerceDBContext db)
        {
            _db = db;
        }

        public ResponseModel AdminLogin(LoginModel loginModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var userData = _db.AdminInfos.Where(x => x.Email == loginModel.EmailId).FirstOrDefault();
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
            /*
            ResponseModel responseModel = new ResponseModel();
            responseModel.Status = false;
            responseModel.Message = "Incorrect Id/Password";
            return responseModel;
            */
        }


        //public AdminModel GetAdminLogin(LoginModel loginModel)
        //{
        //    ResponseModel response = new ResponseModel();

        //        var userData = _db.AdminInfos.Where(x => x.Email == loginModel.EmailId).FirstOrDefault();
        //        AdminModel adminInfo = new AdminModel();
        //        if (userData.Password == loginModel.Password)
        //         {
        //                adminInfo.Id = userData.Id;
        //                adminInfo.Name = userData.Name;
        //                adminInfo.Email = userData.Email;
        //                adminInfo.Password = userData.Password;
        //                response.Status = true;
        //                response.Message = Convert.ToString(userData.Id) + "|" + userData.Name + "|" + userData.Email;                       
                   
        //        }
        //       return adminInfo;


        // }


    public CategoryModel SaveCategory(CategoryModel newCategory)
        {
            try
            {
                Category _category = new Category();
                _category.Name = newCategory.Name;
                _db.Add(_category);
                _db.SaveChanges();
                return newCategory;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<CategoryModel> GetCategories()
        {
            var data = _db.Categories.ToList();
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

        public bool UpdateCategory(CategoryModel categoryToUpdate)
        {
            bool flag = false;
            var _category = _db.Categories.Where(x => x.Id == categoryToUpdate.Id).First();
            if (_category != null)
            {
                _category.Name = categoryToUpdate.Name;
                _db.Categories.Update(_category);
                _db.SaveChanges();
                flag = true;
            }
            return flag;
        }

        public bool DeleteCategory(CategoryModel categoryToDelete)
        {
            bool flag = false;
            var _category = _db.Categories.Where(x => x.Id == categoryToDelete.Id).First();
            if (_category != null)
            {
                _db.Categories.Remove(_category);
                _db.SaveChanges();
                flag = true;
            }
            return flag;
        }

        public List<ProductModel> GetProducts()
        {
            List<Category> categoryData = _db.Categories.ToList();
            List<Product> productData = _db.Products.ToList();
            List<ProductModel> _productList = new List<ProductModel>();
            foreach (var c in productData)
            {
                ProductModel _productModel = new ProductModel();
                _productModel.Id = c.Id;
                _productModel.Name = c.Name;
                _productModel.Price = c.Price;
                _productModel.Stock = c.Stock;
                _productModel.ImageUrl = c.ImageUrl;
                _productModel.CategoryId = c.CategoryId;
                _productModel.CategoryName = categoryData.Where(x => x.Id == c.CategoryId).Select(x => x.Name).FirstOrDefault();
                _productList.Add(_productModel);
            }
            return _productList;
        }

        public bool DeleteProduct(ProductModel productToDelete)
        {
            bool flag = false;
            var _product = _db.Products.Where(x => x.Id == productToDelete.Id).First();
            if (_product != null)
            {
                _db.Products.Remove(_product);
                _db.SaveChanges();
                flag = true;
            }
            return flag;
        }

        public ProductModel SaveProduct(ProductModel newProduct)
        {
            try
            {
                Product _product = new Product();
                _product.Name = newProduct.Name;
                _product.Price = newProduct.Price;
                _product.ImageUrl = newProduct.ImageUrl;
                _product.CategoryId = newProduct.CategoryId;
                _product.Stock = newProduct.Stock;
                _db.Add(_product);
                _db.SaveChanges();
                return newProduct;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetNewProductId()
        {
            try
            {
                int nextProductId = _db.Products.ToList().OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                return nextProductId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<StockModel> GetProductStock()
        {
            List<StockModel> productStock = new List<StockModel>();

            List<Category> categoryData = _db.Categories.ToList();
            List<Product> productData = _db.Products.ToList();
            foreach (var c in productData)
            {
                StockModel _productModel = new StockModel();
                _productModel.Id = c.Id;
                _productModel.Name = c.Name;
                _productModel.Stock = c.Stock;
                _productModel.CategoryName = categoryData.Where(x => x.Id == c.CategoryId).Select(x => x.Name).FirstOrDefault();
                productStock.Add(_productModel);
            }
            return productStock;
        }

        public bool UpdateProductStock(StockModel stock)
        {
            bool flag = false;
            var _product = _db.Products.Where(x => x.Id == stock.Id).First();
            if (_product != null)
            {
                _product.Stock = stock.Stock + stock.NewStock;
                _db.Products.Update(_product);
                _db.SaveChanges();
                flag = true;
            }
            return flag;
        }
            
    }
}
