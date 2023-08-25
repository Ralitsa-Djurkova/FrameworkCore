using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StudentProject.Commen;

namespace StudentProject.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [StringLength(ConstantsValue.MAX_NAME_STRUDENT)]
        public string? Name { get; set; }
        [ForeignKey(nameof(Course))]
        public int CourseId;
        [ForeignKey(nameof(School))]
        public int SchoolId { get; set; }
    }
}
