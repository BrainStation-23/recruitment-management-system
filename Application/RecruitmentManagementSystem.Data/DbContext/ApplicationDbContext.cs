using System.Data.Entity;
using System.Web.Configuration;
using Microsoft.AspNet.Identity.EntityFramework;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(WebConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString, false)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
    }
}