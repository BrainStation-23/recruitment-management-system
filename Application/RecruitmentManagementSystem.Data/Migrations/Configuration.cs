using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DbContext.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DbContext.ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                const string role = "SuperAdmin";
                const string email = "admin-rms@bs-23.com";
                const string password = "007@HakunaMatata";

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                var user = new ApplicationUser
                {
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = email,
                    Email = email
                };

                var result = userManager.Create(user, password);

                if (result.Succeeded)
                {
                    if (!roleManager.RoleExists(role))
                    {
                        roleManager.Create(new IdentityRole(role));
                    }
                    userManager.AddToRole(user.Id, role);
                }
            }

            base.Seed(context);
        }
    }
}