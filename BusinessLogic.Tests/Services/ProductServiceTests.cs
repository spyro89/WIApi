using BusinessLogic.Dto.Product;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services;
using DB.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Tests.Services
{
    [TestFixture]
    internal class ProductServiceTests
    {
        private Mock<IProductRepository> productRepositoryMock;
        private Mock<IOrderRepository> orderRepositoryMock;

        private ProductService productService;

        [SetUp]
        public void SetUp()
        {
            productRepositoryMock = new();
            orderRepositoryMock = new();

            productService = new(productRepositoryMock.Object, orderRepositoryMock.Object);
        }

        // Metoda_PrzypadekTestowy_RetursnWynik

        [Test]
        public void Update_WhenProductDoesntExist_ReturnFalse()
        {
            // arrange
            var productToBeUpdatedId = 123;
            var updatedProductDto = new ProductAddEditDto()
            {
                Name = "newName",
                Price = 12,
                Quantity = 1
            };

            productRepositoryMock
                .Setup(p => p.GetOne(productToBeUpdatedId))
                .Returns((Product)null);

            // act
            var serviceResult = productService.Update(productToBeUpdatedId, updatedProductDto);

            // assert
            Assert.That(serviceResult, Is.False);
            productRepositoryMock
                .Verify(p => p.GetOne(productToBeUpdatedId), Times.Once);
        }

        [Test]
        public void Update_ProductExistsAndUpdates_ReturnTrue()
        {
            // arrange
            var productToBeUpdatedId = 123;
            var newProductName = "newName";
            var newProductPrice = 5;
            short newProductQuantity = 51;

            var updatedProductDto = new ProductAddEditDto()
            {
                Name = newProductName,
                Price = newProductPrice,
                Quantity = newProductQuantity
            };

            var existingProduct = new Product()
            {
                Id = productToBeUpdatedId,
                Name = "oldName",
                Price = 1,
                Quantity = 10
            };

            productRepositoryMock
                .Setup(p => p.GetOne(productToBeUpdatedId))
                .Returns(existingProduct);

            // act
            var serviceResult = productService.Update(productToBeUpdatedId, updatedProductDto);
            
            // assert
            Assert.Multiple(() =>
            {
                Assert.That(serviceResult, Is.True);

                Assert.That(existingProduct, Is.Not.Null);
                Assert.That(existingProduct.Name, Is.EqualTo(newProductName));
                Assert.That(existingProduct.Price, Is.EqualTo(newProductPrice));
                Assert.That(existingProduct.Quantity, Is.EqualTo(newProductQuantity));
            });

            productRepositoryMock
                .Verify(p => p.GetOne(productToBeUpdatedId), Times.Once);
            productRepositoryMock
                .Verify(p => p.Update(existingProduct), Times.Once);
        }
    }
}
