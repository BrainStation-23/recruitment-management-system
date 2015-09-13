using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecruitmentManagementSystem.Data.DbContext;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.App
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            const string role = "SuperAdmin";
            const string email = "admin-rms@bs-23.com";
            const string password = "007@HakunaMatata";

            if (!context.Users.Any(u => u.Email == email))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                var user = new ApplicationUser
                {
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = email,
                    Email = email
                };

                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new IdentityRole(role));
                }

                var result = userManager.Create(user, password);

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, role);
                }
            }

            base.Seed(context);
        }
    }
}