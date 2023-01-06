using Methanit_ASP_NET_Core_7;
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
    public class FridgeModelsControllerTests
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfModel()
        {
            // Arrange
            var mock = new Mock<IRepository<Fridge_Model>>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestModel());
            var controller = new FridgeModelsController(mock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Fridge_Model>>(viewResult.Model);
            Assert.Equal(GetTestModel().Count, model.Count());
        }

        [Fact]
        public void AddModelReturnsViewResultWithModel()
        {
            // Arrange
            var mock = new Mock<IRepository<Fridge_Model>>();
            var controller = new FridgeModelsController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            Fridge_Model newModel = new Fridge_Model();

            // Act
            var result = controller.Create(newModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newModel, viewResult?.Model);
        }

        [Fact]
        public void AddModelReturnsARedirectAndAddsModel()
        {
            var mock = new Mock<IRepository<Fridge_Model>>();
            var controller = new FridgeModelsController(mock.Object);
            var newModel = new Fridge_Model()
            {
                Name = "GeForce",
                Year = 2022
            };

            var result = controller.Create(newModel);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(r => r.Create(newModel));
        }

        [Fact]
        public void GetModelReturnsNotFoundResultWhenModelNotFound()
        {
            Guid testModelId = Guid.NewGuid();
            var mock = new Mock<IRepository<Fridge_Model>>();
            mock.Setup(repo => repo.GetModel(testModelId))
                .Returns(null as Fridge_Model);
            var controller = new FridgeModelsController(mock.Object);

            var result = controller.Edit(testModelId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetModelReturnsViewResultWithModel()
        {
            Guid testModelId = Guid.Parse("5b6060fa-507b-49e1-adf0-08daee3cfca3");
            var mock = new Mock<IRepository<Fridge_Model>>();
            mock.Setup(repo => repo.GetModel(testModelId))
                .Returns(GetTestModel().First(p => p.Fridge_ModelId == testModelId));
            var controller = new FridgeModelsController(mock.Object);

            var result = controller.Edit(testModelId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Fridge_Model>(viewResult.ViewData.Model);
            Assert.Equal("GeForce", model.Name);
            Assert.Equal(2021, model.Year);
            Assert.Equal(testModelId, model.Fridge_ModelId);
        }


        private List<Fridge_Model> GetTestModel()
        {
            var models = new List<Fridge_Model>
            {
                new Fridge_Model { Fridge_ModelId = Guid.Parse("5b6060fa-507b-49e1-adf0-08daee3cfca3"), Name="GeForce", Year=2021},
                new Fridge_Model { Fridge_ModelId = Guid.NewGuid(), Name="Atlanta", Year=2022},
                new Fridge_Model { Fridge_ModelId = Guid.NewGuid(), Name="Arctic", Year=2020},
                new Fridge_Model { Fridge_ModelId = Guid.NewGuid(), Name="Bottle", Year=2022}
            };
            return models;
        }
    }
}
