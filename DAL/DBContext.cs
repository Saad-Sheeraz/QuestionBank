using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class DBContext : DbContext
    {
        public DBContext()
          : base("name=MyconStr")
        {
        }

        //-----auto--------
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        //-----auto--------


        //---------custom-------------   
        public virtual DbSet<tblUserProfile> tblUserProfiles { get; set; }
        public virtual DbSet<tblDegree> tblDegrees { get; set; }
        public virtual DbSet<tblSubject> tblSubjects { get; set; }
        public virtual DbSet<tblCategory> tblCategories { get; set; }
        public virtual DbSet<tblComplexity> tblComplexities { get; set; }
        public virtual DbSet<tblQuestion> tblQuestions { get; set; }
        public virtual DbSet<tblAnwer> tblAnwers { get; set; }
        public virtual DbSet<tblBook> tblBooks { get; set; }
        public virtual DbSet<tblRighAnswer> tblRighAnswers { get; set; }
        public virtual DbSet<tblChoice> tblChoices { get; set; }
        public virtual DbSet<tblPaperMaker> tblPaperMakers { get; set; }
        public virtual DbSet<tblBookReference> tblBookReferences { get; set; }
        public virtual DbSet<tblReason> tblReasons { get; set; }
        public virtual DbSet<tblUserSubject> tblUserSubjects { get; set; }
       
        //--------custom ends-------------


        //----------Views-------------
        public virtual DbSet<tblViewData> tblViewDatas { get; set; }
        public virtual DbSet<viewForVerfier> viewForVerfiers { get; set; }
        public virtual DbSet<vAlpha> vAlphas { get; set; }
        public virtual DbSet<viewForRole> viewForRoles { get; set; }

        //----------Views-------------


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.tblUserProfiles)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.userid);


            modelBuilder.Entity<AspNetUser>()
              .HasMany(e => e.AspNetUserClaims)
              .WithRequired(e => e.AspNetUser)
              .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);


            modelBuilder.Entity<tblDegree>()
              .HasMany(e => e.tblSubjects)
              .WithOptional(e => e.tblDegree)
              .HasForeignKey(e => e.classId);



            modelBuilder.Entity<tblCategory>()
                .HasMany(e => e.tblQuestions)
                .WithRequired(e => e.tblCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblComplexity>()
                .HasMany(e => e.tblQuestions)
                .WithRequired(e => e.tblComplexity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDegree>()
                .HasMany(e => e.tblCategories)
                .WithOptional(e => e.tblDegree)
                .HasForeignKey(e => e.classId);

            modelBuilder.Entity<tblDegree>()
                .HasMany(e => e.tblQuestions)
                .WithRequired(e => e.tblDegree)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<tblSubject>()
                .HasMany(e => e.tblQuestions)
                .WithRequired(e => e.tblSubject)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<tblComplexity>()
                .HasMany(e => e.tblQuestions)
                .WithRequired(e => e.tblComplexity)
                .WillCascadeOnDelete(false);



        }

        // public System.Data.Entity.DbSet<DAL.tblAnswer> tblAnswers { get; set; }
    }
}




