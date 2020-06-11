using CodeFisrtSampleProject.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFisrtSampleProject.Data
{
    public class OnlineEducationDBContext : DbContext
    {
        public  OnlineEducationDBContext(DbContextOptions options) : base(options)
        {
        }

        #region Add DBSet
        public virtual DbSet<CourseCategory> CourseCategories { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseClass> CourseClasses { get; set; }
        public virtual DbSet<CourseClassDay> CourseClassDays { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CourseCategory>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(128).IsRequired();
            });
            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(128).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(512);// .HasColumnName("MyDescription");
                //entity.ToTable("MyCourse"); //> we can change the name of table in sql db and put it this name
            });
        }
    }
}
