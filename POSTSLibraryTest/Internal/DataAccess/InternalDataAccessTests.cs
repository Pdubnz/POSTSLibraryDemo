using Moq;
using POSTSLibrary.Internal.DataAccess;
using POSTSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Xunit;

namespace POSTSLibrary.Tests.Internal.DataAccess
{
    /// <summary>
    /// This test class is for the InternalDataAccess class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class InternalDataAccessTests
    {
        [Fact]
        public void LoadAllProductData_ReturnsAllProducts()
        {
            var internalDataAccessMock = new Mock<IInternalDataAccess>();
            var expected = 4;
            internalDataAccessMock.Setup(ia => ia.LoadAllProductData())
                        .Returns(
                            new List<ProductModel>() {
                                new ProductModel("A"),
                                new ProductModel("B"),
                                new ProductModel("B"),
                                new ProductModel("B")
                            }
                        );
            var actual = internalDataAccessMock.Object.LoadAllProductData();
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual.Count);
        }
        [Theory]
        [InlineData('A', "A")]
        [InlineData('B', "B")]
        [InlineData('C', "C")]
        [InlineData('D', "D")]
        public void LoadProductData_ReturnsCorrectProduct(char productId, string expected)
        {
            var internalDataAccessMock = new Mock<IInternalDataAccess>();

            internalDataAccessMock.Setup(ia => ia.LoadProductData(productId))
                        .Returns(
                            new List<ProductModel>() {
                                new ProductModel(productId.ToString())
                            }
                        );
            var actual = internalDataAccessMock.Object.LoadProductData(productId);
            Assert.NotEmpty(actual);
            Assert.Single(actual);
            Assert.Equal(expected, actual.First().Id);
        }
    }
}
