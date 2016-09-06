using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASSSK.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int CourseID { get; set; }
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }
        public int Rate1 { get; set; }
        public int Rate2 { get; set; }
        public int Rate3 { get; set; }
        //foreign key
        public int DepartmentID { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual Department Department { get; set; }

    }
}