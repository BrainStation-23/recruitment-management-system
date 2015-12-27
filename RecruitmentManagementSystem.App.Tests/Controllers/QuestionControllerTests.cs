using System.Web.Mvc;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.App.Controllers;
using Xunit;

namespace RecruitmentManagementSystem.App.Tests.Controllers
{
    public class QuestionControllerTests
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IChoiceRepository _choiceRepository;

        [Fact]
        public void List()
        {
            //var result = controller.List();
        }

        [Fact]
        public void CreateGet()
        {
            var controller = new QuestionController(_questionRepository, _fileRepository, _choiceRepository);
            var result = controller.Create() as ViewResult;
            Assert.NotNull(result);
        }
    }
}