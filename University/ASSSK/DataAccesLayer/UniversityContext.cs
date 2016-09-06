using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASSSK.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace ASSSK.DataAccesLayer
{
    public class UniversityContext : DbContext
    {
        public UniversityContext() : base("UniversityContext")
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        //the abstraction class
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //adding the new entities to this class and customize some of the mapping 
            //using fluent API calls.. the API is "fluent" because it's often used by stringing 
            //a series of method calls together into a single statement, as in the following code
            //this code is used to relationship many-to-many
            //OHHH... anybody help me.. i'm in sad and sick
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Teachers).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                .MapRightKey("TeacherID")
                .ToTable("CourseTeacher"));
            //using the srored procedures for inserting, updating and deleting
            modelBuilder.Entity<Department>().MapToStoredProcedures();
            //modelBuilder.Entity<Instructor>()
            //.HasOptional(p => p.OfficeAssignment).WithRequired(p => p.Instructor);
        }



    }
}