namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<tblAnwer> tblAnwers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblAnwer>()
                .Property(e => e.answerText1)
                .IsUnicode(false);

            modelBuilder.Entity<tblAnwer>()
                .Property(e => e.notused)
                .IsUnicode(false);

            modelBuilder.Entity<tblAnwer>()
                .Property(e => e.notused1)
                .IsUnicode(false);

            modelBuilder.Entity<tblAnwer>()
                .Property(e => e.notused2)
                .IsUnicode(false);

            modelBuilder.Entity<tblAnwer>()
                .Property(e => e.answerRight)
                .IsUnicode(false);

            modelBuilder.Entity<tblAnwer>()
                .Property(e => e.createdBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblAnwer>()
                .Property(e => e.updatedBy)
                .IsUnicode(false);
        }
    }
}
