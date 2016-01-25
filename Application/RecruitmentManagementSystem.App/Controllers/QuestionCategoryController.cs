using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using RecruitmentManagementSystem.Data.Interfaces;
using JsonResult = RecruitmentManagementSystem.App.Infrastructure.ActionResults.JsonResult;
using QuestionCategory = RecruitmentManagementSystem.Core.Models.Question.QuestionCategoryDto;
using RecruitmentManagementSystem.Core.Interfaces;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class QuestionCategoryController : BaseController
    {
        private readonly IQuestionCategoryRepository _questionCategoryRepository;

        private readonly IQuestionCategoryService _questionCategoryService;

        public QuestionCategoryController(IQuestionCategoryRepository questionCategoryRepository, IQuestionCategoryService questionCategoryService)
        {
            _questionCategoryRepository = questionCategoryRepository;

            _questionCategoryService = questionCategoryService;
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = _questionCategoryService.GetPagedList();

            if (Request.IsAjaxRequest())
            {
                return new JsonResult(model, JsonRequestBehavior.AllowGet);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionCategory question)
        {
            if (!ModelState.IsValid) return View(question);

            _questionCategoryService.Insert(question);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var model =
                _questionCategoryRepository.FindAll()
                    .ProjectTo<QuestionCategory>()
                    .FirstOrDefault(x => x.Id == id);

            if (model == null) return new HttpNotFoundResult();

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var model =
                _questionCategoryRepository.FindAll()
                    .ProjectTo<QuestionCategory>()
                    .FirstOrDefault(x => x.Id == id);

            if (model == null) return new HttpNotFoundResult();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionCategory model)
        {
            if (!ModelState.IsValid) return View(model);

            _questionCategoryService.Update(model);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _questionCategoryRepository.Delete(id);
            _questionCategoryRepository.Save();

            return RedirectToAction("List");
        }
    }
}