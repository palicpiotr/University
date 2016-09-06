using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASSSK.Models;

namespace ASSSK.ViewModels
{
    public class TeacherIndexData
    {
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}