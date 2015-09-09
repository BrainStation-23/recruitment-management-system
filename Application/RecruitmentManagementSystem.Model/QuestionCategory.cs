using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentManagementSystem.Model
{
    public enum Category
    {
        BasicProgramingC, ProblemSolving, OOP, Java, CSharp, DataStructures, Algorithm, Database, Analytical 
    }
    public class QuestionCategory
    {
        public int ID { get; set; }
        public Category Category { get; set; }
    }
}
