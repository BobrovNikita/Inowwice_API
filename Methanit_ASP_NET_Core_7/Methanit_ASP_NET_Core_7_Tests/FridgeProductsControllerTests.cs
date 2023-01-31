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
    public class FridgeProductsControllerTests
    {
        [Fact]
        public void AddModelReturnsViewResultWithModel()
        {
            // Arrange
            var mock = new Mock<IRepository<Fridge_Products>>();
            var ProductsMock = new Mock<IRepository<Products>>();
            var controller = new FridgeProductsController(mock.Object, ProductsMock.Object);
            controller.ModelState.AddModelError("ProductsId", "Required");
            Fridge_Products newModel = new Fridge_Products();

            // Act
            var result = controller.Add(newModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newModel, viewResult?.Model);
        }

        [Fact]
        public void AddModelReturnsARedirectAndAddsModel()
        {
            var mock = new Mock<IRepository<Fridge_Products>>();
            var productsMock = new Mock<IRepository<Products>>();
            var controller = new FridgeProductsController(mock.Object, productsMock.Object);
            var newModel = new Fridge_Products()
            {
                FridgeId= Guid.NewGuid(),
                ProductsId= Guid.NewGuid(),
                Quantity= 1,
            };

            var result = controller.Add(newModel);

            var redirect = Assert.IsType<RedirectResult>(result);
            Assert.NotNull(redirect.Url);
            Assert.Equal($"~/Fridge/About/{newModel.FridgeId}", redirect.Url);
            mock.Verify(r => r.Create(newModel));
        }

        [Fact]
        public void GetModelReturnsNotFoundResultWhenModelNotFound()
        {
            Guid testModelId = Guid.NewGuid();
            var mock = new Mock<IRepository<Fridge_Products>>();
            var productsMock = new Mock<IRepository<Products>>();
            mock.Setup(repo => repo.GetModel(testModelId))
                .Returns(null as Fridge_Products);
            var controller = new FridgeProductsController(mock.Object, productsMock.Object);
            var result = controller.Edit(testModelId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetModelReturnsViewResultWithModel()
        {
            Guid testModelId = Guid.Parse("0034b8d1-dca7-40e8-1fee-08daed8c6976");
            var mock = new Mock<IRepository<Fridge_Products>>();
            var productsMock = new Mock<IRepository<Products>>();
            mock.Setup(repo => repo.GetModel(testModelId))
                .Returns(GetTestModel().First(p => p.Id == testModelId));
            var controller = new FridgeProductsController(mock.Object, productsMock.Object);

            var result = controller.Edit(testModelId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Fridge_Products>(viewResult.ViewData.Model);
            Assert.Equal(5000, model.Quantity);
            Assert.Equal(Guid.Parse("1e9535bf-1508-47b9-3232-08daed8589e5"), model.FridgeId);
            Assert.Equal(Guid.Parse("ad3a1697-9551-4d7e-68cc-08daed85547e"), model.ProductsId);
            Assert.Equal(testModelId, model.Id);
        }


        private List<Fridge_Products> GetTestModel()
        {
            var models = new List<Fridge_Products>
            {
                new Fridge_Products { Id = Guid.Parse("0034b8d1-dca7-40e8-1fee-08daed8c6976"), FridgeId = Guid.Parse("1e9535bf-1508-47b9-3232-08daed8589e5"), ProductsId = Guid.Parse("ad3a1697-9551-4d7e-68cc-08daed85547e"), Quantity = 5000},
                new Fridge_Products { Id = Guid.NewGuid(), FridgeId = Guid.NewGuid(), ProductsId = Guid.NewGuid(), Quantity = 20},
                new Fridge_Products { Id = Guid.NewGuid(), FridgeId = Guid.NewGuid(), ProductsId = Guid.NewGuid(), Quantity = 500},
                new Fridge_Products { Id = Guid.NewGuid(), FridgeId = Guid.NewGuid(), ProductsId = Guid.NewGuid(), Quantity = 1000}
            };
            return models;
        }
    }
}
