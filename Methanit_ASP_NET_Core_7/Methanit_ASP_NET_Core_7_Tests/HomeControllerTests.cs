using Methanit_ASP_NET_Core_7;
using Methanit_ASP_NET_Core_7.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Methanit_ASP_NET_Core_7_Tests
{
    public class HomeControllerTests
    {
        private ApplicationContext db;
        [Fact]
        public void IndexViewResultNotNull()
        {
            // Arrange
            HomeController controller = new HomeController(db);
            // Act
            ViewResult? result = controller.Index() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexViewNameEqualIndex()
        {
            // Arrange
            HomeController controller = new HomeController(db);

            ViewResult? result = controller.Index() as ViewResult;

            Assert.Equal("Index", result?.ViewName);
        }
    }
}