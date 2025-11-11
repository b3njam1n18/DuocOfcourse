using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Models;
using Api.Repository.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Schools> Schools { get; set; }
        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Attempts> Attempts { get; set; }
        public virtual DbSet<Auth_Credentials> Auth_Credentials { get; set; }
        public virtual DbSet<Certificates> Certificates { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<Course_Categories> Course_Categories { get; set; }
        public virtual DbSet<Course_Modules> Course_Modules { get; set; }
        public virtual DbSet<Course_Progress> Course_Progress { get; set; }
        public virtual DbSet<Enrollments> Enrollments { get; set; }
        public virtual DbSet<Evaluations> Evaluations { get; set; }
        public virtual DbSet<Lessons> Lessons { get; set; }
        public virtual DbSet<Lesson_Progress> Lesson_Progress { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Options> Options { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<Password_Reset_Token> Password_Reset_Tokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AnswersConfiguration());
            modelBuilder.ApplyConfiguration(new AttemptsConfiguration());
            modelBuilder.ApplyConfiguration(new Auth_CredentialsConfiguration());
            modelBuilder.ApplyConfiguration(new CertificatesConfiguration());
            modelBuilder.ApplyConfiguration(new CoursesConfiguration());
            modelBuilder.ApplyConfiguration(new Course_CategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new Course_ModulesConfiguration());
            modelBuilder.ApplyConfiguration(new Course_ProgressConfiguration());
            modelBuilder.ApplyConfiguration(new CoursesConfiguration());
            modelBuilder.ApplyConfiguration(new EnrollmentsConfiguration());
            modelBuilder.ApplyConfiguration(new EvaluationsConfiguration());
            modelBuilder.ApplyConfiguration(new LessonsConfiguration());
            modelBuilder.ApplyConfiguration(new MediaConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationsConfiguration());
            modelBuilder.ApplyConfiguration(new OptionsConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionsConfiguration());
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolsConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new Password_Reset_TokenConfiguration());

        }
    }
    
}
