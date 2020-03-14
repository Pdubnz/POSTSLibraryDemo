using Newtonsoft.Json;
using POSTSLibrary.Internal.DataAccess;
using POSTSLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace POSTSLibrary.DataAccess
{
    /// <summary>
    /// This class is the access the product data only.
    /// </summary>
    public class ProductData : IProductData
    {
        private readonly IInternalDataAccess _dataAccess;
        /// <summary>
        /// Constructor setup for di.
        /// </summary>
        /// <param name="dataAccess">The internal data access.</param>
        public ProductData(IInternalDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>The product that matches the id.</returns>
        public ProductModel GetProductById(char productId)
        {
            return _dataAccess.LoadProductData(productId).FirstOrDefault();
        }
        /// <summary>
        /// Gets all the products from the internal data access.
        /// </summary>
        /// <returns>A list of all the products.</returns>
        public List<ProductModel> GetAllProducts()
        {
            return _dataAccess.LoadAllProductData();
        }
    }
}
