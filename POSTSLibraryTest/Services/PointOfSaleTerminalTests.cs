using POSTSLibrary.DataAccess;
using POSTSLibrary.Internal.DataAccess;
using POSTSLibrary.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Xunit;

namespace POSTSLibraryTest.Services
{
    /// <summary>
    /// This test class is for the PointOfSaleTerminal class.
    /// Need to Mock data access and point of sale terminal.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PointOfSaleTerminalTests
    {
        private readonly PointOfSaleTerminal _pointOfSaleTerminal;
        public PointOfSaleTerminalTests()
        {
            _pointOfSaleTerminal = new PointOfSaleTerminal(new ProductData(new InternalDataAccess()));
        }
        [Theory]
        [InlineData("ABCDABA")]
        [InlineData("ABCABA")]
        [InlineData("ACABA")]
        public void EmptyCart_(string productList)
        {
            foreach (char p in productList)
            {
                _pointOfSaleTerminal.ScanItem(p);
            };
            _pointOfSaleTerminal.EmptyCart();
            var actual = _pointOfSaleTerminal.Cart.Items;
            Assert.Empty(actual);
            _pointOfSaleTerminal.EmptyCart();
        }

        [Theory]
        [InlineData('F')]
        [InlineData('1')]
        [InlineData('/')]
        [InlineData(' ')]
        public void ScanItem_InValidItem_CartHasNoItems(char productId)
        {
            _pointOfSaleTerminal.ScanItem(productId);
            var actual = _pointOfSaleTerminal.Cart.Items;
            Assert.Empty(actual);
            _pointOfSaleTerminal.EmptyCart();
        }
        [Theory]
        [InlineData("A    ", 1)]
        [InlineData("12B57 C", 2)]
        [InlineData("D&C^A@#", 3)]
        [InlineData("  _{} ", 0)]
        [InlineData("%A'B.C+D", 4)]
        public void ScanItem_InValidProductId_ReturnsExpectedItemCount(string productList, int expected)
        {
            productList.ToList().ForEach(p => _pointOfSaleTerminal.ScanItem(p));
            var actual = _pointOfSaleTerminal.Cart.Items.Count;
            Assert.Equal(actual, expected);
            _pointOfSaleTerminal.EmptyCart();
        }

        [Theory]
        [InlineData("ABCDABA", 4)]
        [InlineData("ABCABA", 3)]
        [InlineData("ACABA", 3)]
        [InlineData("ABBA", 2)]
        [InlineData("C", 1)]
        public void ScanItem_ValidProductIds_ReturnsExpectedItemCount(string productList, int expected)
        {
            foreach (char p in productList)
            {
                _pointOfSaleTerminal.ScanItem(p);
            };
            var actual = _pointOfSaleTerminal.Cart.Items.Count;
            Assert.True(actual == expected);
            _pointOfSaleTerminal.EmptyCart();
        }


        [Theory]
        [InlineData("ABCBD", 'B', 1)]
        [InlineData("ABCD", 'A', 0)]
        [InlineData("ABBBD", 'B', 2)]
        [InlineData("ABCDBCDBB", 'B', 3)]
        public void RemoveItem_ValidProductId_ReturnsExpectedItemCount(string productList, char removeProductId, int expected)
        {
            foreach (char p in productList)
            {
                _pointOfSaleTerminal.ScanItem(p);
            };
            _pointOfSaleTerminal.RemoveItem(removeProductId);
            var product = _pointOfSaleTerminal.Cart.Items.Where(p => p._product.Id == removeProductId.ToString()).First();
            int actual = 0;
            if (product != null)
            {
                actual = product.Count;
            }
            Assert.True(actual == expected);
            _pointOfSaleTerminal.EmptyCart();
        }
        [Theory]
        [InlineData("ABCDABA", 13.25)]
        [InlineData("CCCCCCC", 6.00)]
        [InlineData("ABCD", 7.25)]
        public void CalculateTotal_ValidItems_ReturnsExpectedPriceTotal(string productList, double expected)
        {
            foreach (char p in productList)
            {
                _pointOfSaleTerminal.ScanItem(p);
            };
            var actual = _pointOfSaleTerminal.CalculateCartTotal();
            Assert.True(actual == expected);
            _pointOfSaleTerminal.EmptyCart();
        }
    }
}
