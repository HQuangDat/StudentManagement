namespace StudentManagement.Models.Entity
{
    public class Course
    {
        public Guid CourseID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateOnly dateStart { get; set; }
        public DateOnly dateEnd { get; set;}
        
        public ICollection<StudentCourse> studentCourses { get; set; }
    }
}
