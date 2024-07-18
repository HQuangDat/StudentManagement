using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models.Entity;
using System.Reflection.Metadata.Ecma335;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Student student)
        {
            var student_add = new Student
            {
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                dateOfBirth = student.dateOfBirth,
                className = student.className
            };         
            await dbContext.Students.AddAsync(student_add);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Student");
        }

        //List of Students
        [HttpGet]
        public async Task<IActionResult> List() 
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }

        //Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Student student) 
        {
            var student_delete =  dbContext.Students.Find(student.Id);
            if(student_delete != null)
                dbContext.Students.Remove(student_delete);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Student");
        }
    }
}
