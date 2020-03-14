using POSTSLibrary.Models;
using System.Collections.Generic;

namespace POSTSLibrary.DataAccess
{
    public interface IProductData
    {
        List<ProductModel> GetAllProducts();
        ProductModel GetProductById(char productId);
    }
}