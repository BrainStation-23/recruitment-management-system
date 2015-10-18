using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecruitmentManagementSystem.App.Infrastructure.Tasks;
using RecruitmentManagementSystem.Data.DbContext;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.App
{
    public class SeedData : IRunAtInit
    {
        public void Execute()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                if (!dbContext.Users.Any())
                {
                    SeedApplicationUser(dbContext);
                }

                if (!dbContext.QuestionCategories.Any())
                {
                    SeedQuestionCategory(dbContext);
                }

                dbContext.SaveChanges();
            }
        }

        private static void SeedApplicationUser(DbContext dbContext)
        {
            const string password = "HakunaMatata23";

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(dbContext));

            var user = new ApplicationUser
            {
                Email = "admin-rms@bs-23.com",
                UserName = "admin-rms@bs-23.com",
                FirstName = "John",
                LastName = "Doe"
            };

            var result = userManager.Create(user, password);

            if (!result.Succeeded) return;

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbContext));
            var admin = new IdentityRole {Name = "Admin"};

            if (roleManager.RoleExists("Admin")) return;

            roleManager.Create(admin);
            userManager.AddToRole(user.Id, "Admin");
        }

        private static void SeedQuestionCategory(ApplicationDbContext dbContext)
        {
            new List<QuestionCategory>
            {
                new QuestionCategory
                {
                    Name = "OOP",
                    Description = "This section contains questions related to object oriented programming."
                },
                new QuestionCategory
                {
                    Name = "Database",
                    Description = "This section contains questions related to database."
                }
            }.ForEach(category => dbContext.QuestionCategories.Add(category));
        }
    }
}