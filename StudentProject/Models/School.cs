using StudentProject.Commen;
using System.ComponentModel.DataAnnotations;

namespace StudentProject.Models
{
    public class School
    {
        public int SchoolId { get; set; }
        [StringLength(ConstantsValue.MAX_NAME_SCHOOL_LENGTH)]
        public string? SchoolName { get; set; }
    }
}
