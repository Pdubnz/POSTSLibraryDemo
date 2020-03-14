using Moq;
using POSTSLibrary.DataAccess;
using POSTSLibrary.Internal.DataAccess;
using POSTSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Xunit;

namespace POSTSLibraryTest.DataAccess
{
    /// <summary>
    /// This test class is for the ProductData class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProductDataTests
    {
        [Theory]
        [InlineData('A', "A")]
        [InlineData('B', "B")]
        [InlineData('C', "C")]
        [InlineData('D', "D")]
        public void GetProductById_ValidId_ReturnCorrectItem(char productId, string expected)
        {
            var dataAccessMock = new Mock<IProductData>();
            dataAccessMock.Setup(da => da.GetProductById(productId)).Returns(new ProductModel(productId.ToString()));
            Assert.Equal(expected, dataAccessMock.Object.GetProductById(productId).Id);
        }
        [Fact]
        public void GetAllProducts_Valid_ReturnAllItems()
        {
            var expected = 4;
            var dataAccessMock = new Mock<IProductData>();
            dataAccessMock.Setup(da => da.GetAllProducts())
                        .Returns(
                            new List<ProductModel>() {
                                new ProductModel("A"),
                                new ProductModel("B"),
                                new ProductModel("B"),
                                new ProductModel("B")
                            }
                        );
            var actual = dataAccessMock.Object.GetAllProducts();
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual.Count);
        }
    }
}
