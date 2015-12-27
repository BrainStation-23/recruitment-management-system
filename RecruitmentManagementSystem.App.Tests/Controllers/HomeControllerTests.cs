using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void ValueTypeNotEqual()
        {
            IEnumerable<int> number1 = Enumerable.Range(1, 10);
            IEnumerable<int> number2 = Enumerable.Range(1, 10);

            Assert.Equal(number1, number2);
        }
    }
}