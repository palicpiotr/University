using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASSSK.DataAccesLayer;
using ASSSK.Models;
using ASSSK.ViewModels;
using System.Data.Entity.Infrastructure;

namespace ASSSK.Controllers
{
    public class TeacherController : Controller
    {
        private UniversityContext db = new UniversityContext();

        // GET: Teacher
        public ActionResult Index(int? id, int? courseID)
        {
            //the eager loading
            var viewModel = new TeacherIndexData();
            viewModel.Teachers = db.Teachers
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses.Select(c => c.Department))
                .OrderBy(i => i.LastName);

            if (id != null)
            {
                ViewBag.InstructorID = id.Value;
                viewModel.Courses = viewModel.Teachers.Where(
                    i => i.ID == id.Value).Single().Courses;
            }

            if (courseID != null)
            {
                ViewBag.CourseID = courseID.Value;
                viewModel.Enrollments = viewModel.Courses.Where(
                    x => x.CourseID == courseID).Single().Enrollments;
            }
            return View(viewModel);
        }

        // GET: Teacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            var teacher = new Teacher();
            teacher.Courses = new List<Course>();
            PopulateAssignedCourseData(teacher);
            return View();
        }

        /*the HttpPost Create method adds each selected course to the Courses 
        свойства до the template code that checks for validation 
        errors and adds the new instructor to the database.. */

            //this commected code must be deleted..
            //this code is an alternative to doing HttPost Methos..
       /* private ICollection<Course> _courses;
        public virtual ICollection<Course> Courses
        {
            get
            {
                return _courses ?? (_courses = new List<Course>());
            }
            set
            {
                _courses = value;
            }
        }*/


        // POST: Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstName,HireDate,OfficeAssignment")] Teacher teacher, string[] selectedCourses)
        {
           if (selectedCourses != null)
            {
                teacher.Courses = new List<Course>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = db.Courses.Find(int.Parse(course));
                    teacher.Courses.Add(courseToAdd);
                }
            }
           //если все впорядке
           if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedCourseData(teacher);
            return View(teacher);
        }


        //these settings setting up data for a drop-down-list,
        //but i need a the textbox
        // GET: Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .Where(i => i.ID == id)
                .Single();
            PopulateAssignedCourseData(teacher);

            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.OfficeAssignments, "TeacherID", "Location", teacher.ID);
            return View(teacher);
        }

        //this method is used to checking data
        private void PopulateAssignedCourseData(Teacher teacher)
        {
            var allcourses = db.Courses;
            var teacherCourses = new HashSet<int>(teacher.Courses.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allcourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = teacherCourses.Contains(course.CourseID)
                }
                    );
            }
            ViewBag.Courses = viewModel;
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //replaced to my wants
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses /*[Bind(Include = "ID,LastName,FirstName,HireDate")] Teacher teacher*/)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var teacherToUpdate = db.Teachers
               .Include(i => i.OfficeAssignment)
               //includes the courses
               .Include(i => i.Courses)
               .Where(i => i.ID == id)
               .Single();

            if (TryUpdateModel(teacherToUpdate, "",
               new string[] { "LastName", "FirstMName", "HireDate", "OfficeAssignment" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(teacherToUpdate.OfficeAssignment.Location))
                    {
                        teacherToUpdate.OfficeAssignment = null;
                    }

                    //this used to...when we click to Save
                    UpdateTeacherCourses(selectedCourses, teacherToUpdate);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error 
                    ModelState.AddModelError("", "Unable to save changes. Try again it again.");
                }
            }
            PopulateAssignedCourseData(teacherToUpdate);
            return View(teacherToUpdate);
        }

        //this method is used to Save changes in ...
        private void UpdateTeacherCourses(string[] selectedCourses, Teacher teacherToUpdate)
        {
            if (selectedCourses == null)
            {
                teacherToUpdate.Courses = new List<Course>();
                return;
            }

            var selectedCoursesHash = new HashSet<string>(selectedCourses);
            var teacherCourses = new HashSet<int>
                (teacherToUpdate.Courses.Select(c => c.CourseID));

            foreach (var course in db.Courses)
            {
                if (selectedCoursesHash.Contains(course.CourseID.ToString()))
                {
                    if (!teacherCourses.Contains(course.CourseID))
                    {
                        teacherToUpdate.Courses.Add(course);
                    }
                }

                else
                {
                    if (teacherCourses.Contains(course.CourseID))
                    {
                        teacherToUpdate.Courses.Remove(course);
                    }
                }
            }
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers
                .Include(i => i.OfficeAssignment)
                .Where(i => i.ID == id)
                .Single();

            db.Teachers.Remove(teacher);

            var department = db.Departments
                .Where(d => d.TeacherID == id)
                .SingleOrDefault();

            if (department != null)
            {
                department.TeacherID = null;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
