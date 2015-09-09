using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentManagementSystem.Model
{
    public enum Type
    {
        MCQ, Descriptive
    }

    public enum DisplayType
    {
        Radio, ChackBox, TextArea
    }
    public class Question
    {
        public int Id { get; set; }
        public string Tittle { get; set; }                                  
        public Type Type { get; set; }                                      
        public DisplayType DisplayType { get; set; }                        
        public string Images { get; set; }                                  
        public List<string> Answers { get; set; }                           
        public List<string> Choices { get; set; }                           
        public string Creator { get; set; }                                 
        public DateTime CreatedDate { get; set; }                           
        public string Hint { get; set; }                                    
        public virtual QuestionCategory QuestionCategory { get; set; }      
    }
}
