using POSTSLibrary.DataAccess;
using POSTSLibrary.Internal.DataAccess;
using POSTSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSTSLibrary.Services
{
    /// <summary>
    /// This class is the point of sale that controls
    /// items and price for a transaction.
    /// </summary>
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

        private readonly IProductData _productData;
        /// <summary>
        /// This default constructor is only here to test this.
        /// Use di framework and remove this constructor
        /// </summary>
        public PointOfSaleTerminal()
        {
            _productData = new ProductData(new InternalDataAccess());
        }
        /// <summary>
        /// Constructor that uses di.
        /// </summary>
        /// <param name="productData"></param>
        public PointOfSaleTerminal(IProductData productData)
        {
            _productData = productData;
        }
        /// <summary>
        /// Clears the current cart of all the items
        /// </summary>
        public void EmptyCart()
        {
            Cart.Items.Clear();
        }
        /// <summary>
        /// Scans the item into the cart.
        /// </summary>
        /// <param name="productId">Required the id of the product to be scanned.</param>
        public void ScanItem(char productId)
        {
            if (char.IsLetter(productId))
            {
                var p = _productData.GetProductById(productId);
                if (p == null)
                {
                    return;
                }

                if (Cart.Items.Count == 0)
                {
                    Cart.Items.Add(new CartItemModel(p));

                }
                else
                {
                    CartItemModel item = Cart.Items.Where(i => String.Equals(i._product.Id, p.Id)).FirstOrDefault();
                    if (item?.Count > 0)
                    {
                        Cart.Items.ForEach(i => { if (String.Equals(i._product.Id, p.Id)) { i.Count++; } });
                    }
                    else
                    {
                        Cart.Items.Add(new CartItemModel(p));
                    }
                }
            }
        }
        /// <summary>
        /// Remove a product from the cart.
        /// </summary>
        /// <param name="productId">Required the id of the product to removed.</param>
        public void RemoveItem(char productId)
        {
            if (char.IsLetter(productId))
            {
                var product = this.Cart.Items.Where(item => item._product.Id == productId.ToString()).First();
                if (product != null)
                {
                    product.Count--;
                }
            }
        }
        /// <summary>
        /// Calculates the total for the cart.
        /// </summary>
        /// <returns>The total for the cart.</returns>
        public double CalculateCartTotal()
        {
            double cartTotal = 0;
            Cart.Items.ForEach(item =>
            {
                var amount = item.Count;
                var specialQuantity = item._product.Special.Quantity;
                if (amount >= specialQuantity && specialQuantity > 0)
                {
                    int dividend = amount;
                    int quotient = 0;

                    while (dividend >= specialQuantity)
                    {
                        dividend -= specialQuantity;
                        ++quotient;
                    }
                    if (dividend == 0)
                    {
                        cartTotal += (quotient * item._product.Special.BulkPrice);
                    }
                    else
                    {
                        cartTotal += (quotient * item._product.Special.BulkPrice);
                    }
                    amount = dividend;
                }
                if (amount > 0)
                {
                    cartTotal += (amount * item._product.Price);
                }
            });
            return cartTotal;
        }
    }
}
