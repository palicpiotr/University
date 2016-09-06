using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASSSK.Models
{
    public class OfficeAssignment
    {
        [Key]
        [ForeignKey("Teacher")]
        public int TeacherID { get; set; }
        [StringLength(50)]
        [Display(Name ="Office Location")]
        public string Location { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}