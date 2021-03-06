﻿using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using RecruitmentManagementSystem.App.Infrastructure.ActionResults;
using RecruitmentManagementSystem.Core.Models.Institution;
using RecruitmentManagementSystem.Data.Interfaces;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class InstitutionController : BaseController
    {
        private readonly IInstitutionRepository _institutionRepository;

        public InstitutionController(IInstitutionRepository institutionRepository)
        {
            _institutionRepository = institutionRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _institutionRepository.FindAll().ProjectTo<InstitutionModel>();

            return new EnhancedJsonResult(model, JsonRequestBehavior.AllowGet);
        }
    }
}