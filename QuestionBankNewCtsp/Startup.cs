using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using QuestionBankNewCtsp.Models;


[assembly: OwinStartupAttribute(typeof(QuestionBankNewCtsp.Startup))]
namespace QuestionBankNewCtsp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        // In this method we will create default User roles and Admin user for login
        private void createRolesandUsers()
        {
            AppUsersDbContext context = new AppUsersDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User 

            if (!roleManager.RoleExists("SuperAdmin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "SuperAdmin";
                user.Email = "superadmin@gmail.com";

                string userPWD = "Pakistan@006";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "SuperAdmin");

                }

            }
            // creating creator role
            if (!roleManager.RoleExists("Creator"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Creator";
                roleManager.Create(role);
            }
            // creating verifier role
            if (!roleManager.RoleExists("Verifier"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Verifier";
                roleManager.Create(role);

            }

            //}
            // creating approver role
            if (!roleManager.RoleExists("Approver"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Approver";
                roleManager.Create(role);

            }

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

            }
        }


    }
}