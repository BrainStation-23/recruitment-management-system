using System;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using RecruitmentManagementSystem.App.Infrastructure.ActionResults;
using RecruitmentManagementSystem.Core.Models.Course;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class CourseController : BaseController
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public ActionResult List()
        {
            var models = _courseRepository.FindAll().ProjectTo<CourseModel>();

            if (Request.IsAjaxRequest())
            {
                return new EnhancedJsonResult(models, JsonRequestBehavior.AllowGet);
            }

            return View(models);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _courseRepository.Insert(new Course
            {
                Title = model.Title,
                Description = model.Description,
                CreatedBy = User.Identity.GetUserId(),
                UpdatedBy = User.Identity.GetUserId(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            _courseRepository.Save();

            return RedirectToAction("List");
        }
    }
}