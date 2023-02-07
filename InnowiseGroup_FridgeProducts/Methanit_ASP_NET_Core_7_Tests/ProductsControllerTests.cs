using Methanit_ASP_NET_Core_7.Controllers;
using Methanit_ASP_NET_Core_7.Models;
using Methanit_ASP_NET_Core_7.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methanit_ASP_NET_Core_7_Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfProduct()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestModel());
            var controller = new ProductsController(mock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Equal(GetTestModel().Count, model.Count());
        }

        [Fact]
        public void AddProductReturnsViewResultWithProduct()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            var controller = new ProductsController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            Product newModel = new Product();

            // Act
            var result = controller.Create(newModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newModel, viewResult?.Model);
        }

        [Fact]
        public void AddProductReturnsARedirectAndAddsProduct()
        {
            var mock = new Mock<IRepository<Product>>();
            var controller = new ProductsController(mock.Object);
            var newModel = new Product()
            {
                Name = "Молоко",
                Default_Quantity = 20
            };

            var result = controller.Create(newModel);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(r => r.Create(newModel));
        }

        [Fact]
        public void GetProductReturnsNotFoundResultWhenProductNotFound()
        {
            Guid testModelId = Guid.NewGuid();
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(repo => repo.GetModel(testModelId))
                .Returns(null as Product);
            var controller = new ProductsController(mock.Object);

            var result = controller.Edit(testModelId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetProductReturnsViewResultWithProduct()
        {
            Guid testModelId = Guid.Parse("ad3a1697-9551-4d7e-68cc-08daed85547e");
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(repo => repo.GetModel(testModelId))
                .Returns(GetTestModel().First(p => p.ProductsId == testModelId));
            var controller = new ProductsController(mock.Object);

            var result = controller.Edit(testModelId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Product>(viewResult.ViewData.Model);
            Assert.Equal("Молоко", model.Name);
            Assert.Equal(20, model.Default_Quantity);
            Assert.Equal(testModelId, model.ProductsId);
        }


        private List<Product> GetTestModel()
        {
            var models = new List<Product>
            {
                new Product { ProductsId = Guid.Parse("ad3a1697-9551-4d7e-68cc-08daed85547e"), Name="Молоко", Default_Quantity=20 },
                new Product { ProductsId = Guid.NewGuid(), Name="Atlanta", Default_Quantity= 30},
                new Product { ProductsId = Guid.NewGuid(), Name="Arctic", Default_Quantity= 40},
                new Product { ProductsId = Guid.NewGuid(), Name="Bottle", Default_Quantity= 50}
            };
            return models;
        }
    }
}
