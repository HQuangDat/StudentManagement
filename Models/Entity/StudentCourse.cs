namespace StudentManagement.Models.Entity
{
    public class StudentCourse
    {
        public Guid StudentId { get; set; }
        public Student student { get; set; }

        public Guid CourseId { get; set; }
        public Course course { get; set; }
    }
}
