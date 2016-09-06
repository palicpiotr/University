namespace ASSSK.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using ASSSK.Models;
    using ASSSK.DataAccesLayer;

    internal sealed class Configuration : DbMigrationsConfiguration<ASSSK.DataAccesLayer.UniversityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ASSSK.DataAccesLayer.UniversityContext context)
        {
            //there is the test data of student properties
            var students = new List<Student>
            {
                new Student { FirstName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { FirstName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { FirstName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-08-11") }
            };
            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            //there is the test data of teacher properties
            var teachers = new List<Teacher>
            {
                new Teacher {FirstName = "Kim", LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-01-11")},
                new Teacher { FirstName = "Fadi",    LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Teacher { FirstName = "Roger",   LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Teacher { FirstName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Teacher { FirstName = "Roger",   LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }

            };
            teachers.ForEach(s => context.Teachers.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            //there is the test data of the department properties
            var departments = new List<Department>
            {
                 new Department { Name = "English",     EmployeQuantity = 20,
                    StartDate = DateTime.Parse("2007-09-01"),
                    TeacherID  = teachers.Single( i => i.LastName == "Abercrombie").ID },

                new Department { Name = "Mathematics", EmployeQuantity = 19,
                    StartDate = DateTime.Parse("2007-09-01"),
                    TeacherID  = teachers.Single( i => i.LastName == "Fakhouri").ID },

                new Department { Name = "Engineering", EmployeQuantity = 17,
                    StartDate = DateTime.Parse("2007-09-01"),
                    TeacherID  = teachers.Single( i => i.LastName == "Harui").ID },

                new Department { Name = "Economics",   EmployeQuantity = 25,
                    StartDate = DateTime.Parse("2007-09-01"),
                    TeacherID  = teachers.Single( i => i.LastName == "Kapoor").ID }
            };

            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            //there is the test data of courses
            var courses = new List<Course>
            {
                new Course {CourseID = 1050, Title = "Chemistry",  Rate1= 13, Rate2=17, Rate3=19    ,
                  DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID,
                  Teachers = new List<Teacher>()
                },
                new Course {CourseID = 4022, Title = "Microeconomics", Rate1= 13, Rate2=17, Rate3=19,
                  DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID,
                  Teachers = new List<Teacher>()
                },
                new Course {CourseID = 4041, Title = "Macroeconomics", Rate1= 13, Rate2=17, Rate3=19,
                  DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID,
                  Teachers = new List<Teacher>()
                },
                new Course {CourseID = 1045, Title = "Calculus",       Rate1= 13, Rate2=17, Rate3=19,
                  DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID,
                  Teachers = new List<Teacher>()
                },
                new Course {CourseID = 3141, Title = "Trigonometry",   Rate1= 13, Rate2=17, Rate3=19,
                  DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID,
                  Teachers = new List<Teacher>()
                },
                new Course {CourseID = 2021, Title = "Composition",    Rate1= 13, Rate2=17, Rate3=19,
                  DepartmentID = departments.Single( s => s.Name == "English").DepartmentID,
                 Teachers = new List<Teacher>()
                },
                new Course {CourseID = 2042, Title = "Literature",     Rate1= 13, Rate2=17, Rate3=19,
                  DepartmentID = departments.Single( s => s.Name == "English").DepartmentID,
                  Teachers = new List<Teacher>()
                },
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            context.SaveChanges();


            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment {
                    TeacherID = teachers.Single( i => i.LastName == "Fakhouri").ID,
                    Location = "Belokonskoy 5" },
                new OfficeAssignment {
                    TeacherID = teachers.Single( i => i.LastName == "Harui").ID,
                    Location = "Belokonskoy 5" },
                new OfficeAssignment {
                    TeacherID = teachers.Single( i => i.LastName == "Kapoor").ID,
                    Location = "Belokonskoy 5" },
            };

            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.TeacherID, s));
            context.SaveChanges();

            //calling some methods

            AddOrUpdateTeacher(context, "Chemistry", "Kapoor");
            AddOrUpdateTeacher(context, "Chemistry", "Harui");
            AddOrUpdateTeacher(context, "Microeconomics", "Zheng");
            AddOrUpdateTeacher(context, "Macroeconomics", "Zheng");

            AddOrUpdateTeacher(context, "Calculus", "Fakhouri");
            AddOrUpdateTeacher(context, "Trigonometry", "Harui");
            AddOrUpdateTeacher(context, "Composition", "Abercrombie");
            AddOrUpdateTeacher(context, "Literature", "Abercrombie");

            context.SaveChanges();

            //there is the test data of enrollments

            var enrollments = new List<Enrollment>
            {
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    Group = Group.IST112
                },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    Group = Group.IST112
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    Group = Group.IST112
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    Group = Group.IST112
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    Group = Group.IST112
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    Group = Group.IST112
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    Group = Group.IST113
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                    Group = Group.IST113
                 },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                    Group = Group.IST113
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Li").ID,
                    CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                   Group = Group.IST113
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Justice").ID,
                    CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                    Group = Group.IST113
                 }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                    s.Student.ID == e.StudentID &&
                    s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }

            context.SaveChanges();
        }

        void AddOrUpdateTeacher(UniversityContext context, string courseTitle, string teacherName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var tchr = crs.Teachers.SingleOrDefault(i => i.LastName == teacherName);
            if (tchr == null)
                crs.Teachers.Add(context.Teachers.Single(i => i.LastName == teacherName));
        }
        //there is test data of the courses
        //this code must be uptated
        /* var courses = new List<Course>
             {
                 new Course {CourseID = 1050, Title = "Chemistry",    Rate1= 13, Rate2=17, Rate3=19, },
                 new Course {CourseID = 4022, Title = "Microeconomics", Rate1= 11, Rate2=1, Rate3=1, },
                 new Course {CourseID = 4041, Title = "Macroeconomics", Rate1= 3, Rate2=11, Rate3=19, },
                 new Course {CourseID = 1045, Title = "Calculus",       Rate1= 10, Rate2=18, Rate3=23, },
                 new Course {CourseID = 3141, Title = "Trigonometry",   Rate1= 12, Rate2=17, Rate3=13, },
                 new Course {CourseID = 2021, Title = "Composition",    Rate1= 9, Rate2=7, Rate3=11, },
                 new Course {CourseID = 2042, Title = "Literature",     Rate1= 1, Rate2=7, Rate3=3, }
             };
             courses.ForEach(s => context.Courses.AddOrUpdate(p => p.Title, s));
             context.SaveChanges();

             var enrollments = new List<Enrollment>
             {
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alexander").ID,
                     CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                     Group = Group.IST112
                 },
                  new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alexander").ID,
                     CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                     Group = Group.IST112
                  },
                  new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alexander").ID,
                     CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                     Group = Group.IST112
                  },
                  new Enrollment {
                      StudentID = students.Single(s => s.LastName == "Alonso").ID,
                     CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                     Group = Group.IST112
                  },
                  new Enrollment {
                      StudentID = students.Single(s => s.LastName == "Alonso").ID,
                     CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    Group = Group.IST112
                  },
                  new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                     CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                     Group = Group.IST112
                  },
                  new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Anand").ID,
                     CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                     Group = Group.IST113
                  },
                  new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Anand").ID,
                     CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                     Group = Group.IST113
                  },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
                     CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                     Group = Group.IST113
                  },
                  new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Li").ID,
                     CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                     Group = Group.IST113
                  },
                  new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Justice").ID,
                     CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                     Group = Group.IST113
                  }
             };


             foreach (Enrollment e in enrollments)
             {
                 var enrollmentInDataBase = context.Enrollments.Where(
                     s =>
                          s.Student.ID == e.StudentID &&
                          s.Course.CourseID == e.CourseID).SingleOrDefault();
                 if (enrollmentInDataBase == null)
                 {
                     context.Enrollments.Add(e);
                 }
             }
             context.SaveChanges();*/




    }
}
