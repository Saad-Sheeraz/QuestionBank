namespace QuestionBankNewCtsp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QuestionBankNewCtsp.Models.AppUsersDbContext>
    {
        public Configuration()
        {
           
            AutomaticMigrationsEnabled = true;
            ContextKey = "QuestionBankNewCtsp.Models.AppUsersDbContext";
        }

        protected override void Seed(QuestionBankNewCtsp.Models.AppUsersDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
