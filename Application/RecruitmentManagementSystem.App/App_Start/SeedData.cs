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
                if (!dbContext.Roles.Any())
                {
                    SeedApplicaionRole(dbContext);
                }

                if (!dbContext.Users.Any())
                {
                    SeedApplicationUser(dbContext);
                }

                if (!dbContext.QuestionCategories.Any())
                {
                    SeedQuestionCategory(dbContext);
                }

                if (!dbContext.Institutions.Any())
                {
                    SeedInstitution(dbContext);
                }

                if (!dbContext.JobPositions.Any())
                {
                    SeedJobPosition(dbContext);
                }

                dbContext.SaveChanges();
            }
        }

        private static void SeedApplicaionRole(DbContext dbContext)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbContext));

            roleManager.Create(new IdentityRole {Name = "System Admin"});
            roleManager.Create(new IdentityRole {Name = "Admin"});
            roleManager.Create(new IdentityRole {Name = "Examiner"});
            roleManager.Create(new IdentityRole {Name = "Candidate"});
        }

        private static void SeedApplicationUser(DbContext dbContext)
        {
            const string email = "admin-rms@bs-23.com";
            const string password = "HakunaMatata-23";

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(dbContext));

            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                FirstName = "John",
                LastName = "Doe"
            };

            var result = userManager.Create(user, password);

            if (!result.Succeeded) return;

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

        private static void SeedInstitution(ApplicationDbContext dbContext)
        {
            new List<Institution>
            {
                new Institution
                {
                    Name = "Ahsanullah University of Science & Technology",
                    City = "Dhaka"
                },
                new Institution
                {
                    Name = "American International University Bangladesh",
                    City = "Dhaka"
                },
                new Institution
                {
                    Name = "Bangladesh University of Engineering and Technology",
                    City = "Dhaka",
                },
                new Institution
                {
                    Name = "BRAC University",
                    City = "Dhaka"
                },
                new Institution
                {
                    Name = "Chittagong University of Engineering and Technology",
                    City = "Chittagong"
                },
                new Institution
                {
                    Name = "Dhaka University",
                    City = "Dhaka"
                },
                new Institution
                {
                    Name = "Dhaka University of Engineering & Technology",
                    City = "Gazipur"
                },
                new Institution
                {
                    Name = "East West University",
                    City = "Dhaka"
                },
                new Institution
                {
                    Name = "North South University",
                    City = "Dhaka"
                },
                new Institution
                {
                    Name = "Shahjalal University of Science & Technology",
                    City = "Sylhet"
                },
                new Institution
                {
                    Name = "Stamford University Bangladesh",
                    City = "Dhaka"
                }
            }.ForEach(institution => dbContext.Institutions.Add(institution));
        }

        private static void SeedJobPosition(ApplicationDbContext dbContext)
        {
            new List<JobPosition>
            {
                new JobPosition
                {
                    Name = "Junior Software Engineer",
                    Description = "Lorem Ipsum."
                },
                new JobPosition
                {
                    Name = "Senior Software Engineer",
                    Description = "Lorem Ipsum."
                }
            }.ForEach(row => dbContext.JobPositions.Add(row));
        }
    }
}