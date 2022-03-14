using Shop.DataModels.CustomModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Logic.Services
{
    public interface IAdminService
    {
        ResponseModel AdminLogin(LoginModel loginModel);

        CategoryModel SaveCategory(CategoryModel newCategory);

        List<CategoryModel> GetCategories();

        bool DeleteCategory(CategoryModel categoryToDelete);

        bool UpdateCategory(CategoryModel categoryToUpdate);

        List<ProductModel> GetProducts();

        bool DeleteProduct(ProductModel productToDelete);

        int GetNewProductId();

        ProductModel SaveProduct(ProductModel newProduct);

        List<StockModel> GetProductStock();

        bool UpdateProductStock(StockModel stock);
    }
}
