using System.Collections.Generic;
using AutoMapper;
using Microsoft.Build.Framework.XamlTypes;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.Question
{
    public class QuestionModel : QuestionBase, IMapFrom<Model.Question>
    {

    }
}