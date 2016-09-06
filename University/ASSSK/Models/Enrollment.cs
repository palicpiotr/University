using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASSSK.Models
{
    public enum Group
    {
        IST113, IST112
    }
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        //foreign key references to Course table
        public int CourseID { get; set; }
        //foreign key references to Student table
        public int StudentID { get; set; }
        //static objects in enum Group
        public Group? Group { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }

    }
}