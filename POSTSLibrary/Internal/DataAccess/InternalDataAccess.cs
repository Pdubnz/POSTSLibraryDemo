using POSTSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSTSLibrary.Internal.DataAccess
{
    /// <summary>
    /// This class does the internal access of all items.
    /// </summary>
    public class InternalDataAccess : IInternalDataAccess
    {
        private readonly List<ProductModel> _productList = new List<ProductModel>();

        /// <summary>
        /// Constructor to setup a list of products.
        /// </summary>
        public InternalDataAccess()
        {
            _productList.Add(new ProductModel("A", 1.25, new BulkSpecial(3, 3.00)));
            _productList.Add(new ProductModel("B", 4.25, new BulkSpecial(0, 0.00)));
            _productList.Add(new ProductModel("C", 1.00, new BulkSpecial(6, 5.00)));
            _productList.Add(new ProductModel("D", 0.75, new BulkSpecial(0, 0.00)));
        }
        /// <summary>
        /// Gets all the products.
        /// </summary>
        /// <returns>A list of all the products.</returns>
        public List<ProductModel> LoadAllProductData()
        {
            return _productList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId">Required the id o fthe product to retrieve.</param>
        /// <returns></returns>
        public List<ProductModel> LoadProductData(char productId)
        {
            return _productList.Where(p => p.Id == productId.ToString()).ToList();
        }
    }
}
