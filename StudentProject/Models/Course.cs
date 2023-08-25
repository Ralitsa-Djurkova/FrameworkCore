using Microsoft.VisualBasic;
using StudentProject.Commen;
using System.ComponentModel.DataAnnotations;

namespace StudentProject.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [StringLength(ConstantsValue.MAX_NAME_COURSE_LENGTH)]
        public string? CourseName { get; set; }
    }
}
