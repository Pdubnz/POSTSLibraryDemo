using POSTSLibrary.Models;
using System.Collections.Generic;

namespace POSTSLibrary.Internal.DataAccess
{
    public interface IInternalDataAccess
    {
        List<ProductModel> LoadProductData(char productId);
        List<ProductModel> LoadAllProductData();
    }
}