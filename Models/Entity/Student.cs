﻿namespace StudentManagement.Models.Entity
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly dateOfBirth { get; set; }
        public string className { get; set; }   

        public Guid CourseId { get; set; }
        public ICollection<Course> Courses{ get; set; }
    }
}
