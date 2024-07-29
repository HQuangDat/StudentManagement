using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models.Entity
{
    public class Student
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public DateOnly dateOfBirth { get; set; }
        public string className { get; set; }   

        public ICollection<StudentCourse> studentCourses{ get; set; } = new List<StudentCourse>();
    }
}
