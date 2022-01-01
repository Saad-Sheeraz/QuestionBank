using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QuestionBankNewCtsp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string RollId { get; set; }
        public int SubId { get; set; }
        //for disabling user
        public bool isEnable { get; set; }

        //public string profilePic { get; set; }
        //public string subjectSpecialize { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}

    public class AppUsersDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppUsersDbContext()
            : base("MyconStr", throwIfV1Schema: false)
        {
        }

        public static AppUsersDbContext Create()
        {
            return new AppUsersDbContext();
        }
    }
}