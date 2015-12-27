using System.Web.Mvc;
using RecruitmentManagementSystem.App.Controllers;
using Xunit;

namespace RecruitmentManagementSystem.App.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index()
        {
            var controller = new HomeController();

            var result = controller.Index() as ViewResult;

            Assert.NotNull(result);
        }
    }
}