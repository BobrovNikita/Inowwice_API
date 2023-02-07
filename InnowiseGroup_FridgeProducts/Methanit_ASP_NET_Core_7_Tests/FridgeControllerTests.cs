using Methanit_ASP_NET_Core_7.Controllers;
using Methanit_ASP_NET_Core_7.Models;
using Methanit_ASP_NET_Core_7.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methanit_ASP_NET_Core_7_Tests
{
    public class FridgeControllerTests
    {

        [Fact]
        public void IndexReturnsAViewResultWithAListOfModel()
        {
            // Arrange
            var mock = new Mock<IRepository<Fridge>>();
            var fridgeModelsMock = new Mock<IRepository<Fridge_Model>>();
            var productsMock = new Mock<IRepository<Product>>();
            var fridgeProductsMock = new Mock<IRepository<Fridge_Products>>();
            var webHostMock = new Mock<IWebHostEnvironment>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestModel());
            var controller = new FridgeController(mock.Object, fridgeModelsMock.Object, productsMock.Object, fridgeProductsMock.Object, webHostMock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Fridge>>(viewResult.Model);
            Assert.Equal(GetTestModel().Count, model.Count());
        }
        [Fact]
        public void GetModelReturnsNotFoundResultWhenModelNotFound()
        {
            Guid testModelId = Guid.NewGuid();
            var mock = new Mock<IRepository<Fridge>>();
            var fridgeModelsMock = new Mock<IRepository<Fridge_Model>>();
            var productsMock = new Mock<IRepository<Product>>();
            var fridgeProductsMock = new Mock<IRepository<Fridge_Products>>();
            var webHostMock = new Mock<IWebHostEnvironment>();
            mock.Setup(repo => repo.GetModel(testModelId))
                .Returns(null as Fridge);
            var controller = new FridgeController(mock.Object, fridgeModelsMock.Object, productsMock.Object, fridgeProductsMock.Object, webHostMock.Object);

            var result = controller.Edit(testModelId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetModelReturnsViewResultWithModel()
        {
            Guid testModelId = Guid.Parse("1e9535bf-1508-47b9-3232-08daed8589e5");
            var mock = new Mock<IRepository<Fridge>>();
            var fridgeModelsMock = new Mock<IRepository<Fridge_Model>>();
            var productsMock = new Mock<IRepository<Product>>();
            var fridgeProductsMock = new Mock<IRepository<Fridge_Products>>();
            var webHostMock = new Mock<IWebHostEnvironment>();
            mock.Setup(repo => repo.GetModel(testModelId))
                .Returns(GetTestModel().First(p => p.FridgeId == testModelId));
            var controller = new FridgeController(mock.Object, fridgeModelsMock.Object, productsMock.Object, fridgeProductsMock.Object, webHostMock.Object);

            var result = controller.Edit(testModelId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Fridge>(viewResult.ViewData.Model);
            Assert.Equal("Атланта", model.Name);
            Assert.Equal("Никита", model.Owner_Name);
            Assert.Equal(Guid.Parse("5c44523a-2b48-48d1-ed52-08daed856433"), model.Fridge_ModelId);
            Assert.Equal(testModelId, model.FridgeId);
        }


        private List<Fridge> GetTestModel()
        {
            var models = new List<Fridge>
            {
                new Fridge { FridgeId = Guid.Parse("1e9535bf-1508-47b9-3232-08daed8589e5"), Name="Атланта", Owner_Name = "Никита", Image = "c33dcff8-a47f-4e26-9faa-ec75fe54d3c9.jpg", Fridge_ModelId = Guid.Parse("5c44523a-2b48-48d1-ed52-08daed856433")},
                new Fridge { FridgeId = Guid.NewGuid(), Name="GeForce", Owner_Name = "Дима", Image = "Holla.jpg", Fridge_ModelId = Guid.NewGuid()},
                new Fridge { FridgeId = Guid.NewGuid(), Name="Балтика", Owner_Name = "Игорь", Image = "Sup.jpg", Fridge_ModelId = Guid.NewGuid()},
                new Fridge { FridgeId = Guid.NewGuid(), Name="Kestrel", Owner_Name = "Артур", Image = "1415.jpg", Fridge_ModelId = Guid.NewGuid()}
            };
            return models;
        }
    }
}
