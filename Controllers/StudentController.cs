using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models.Entity;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Add (GET)
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Courses = await dbContext.Courses.ToListAsync();
            return View();
        }

        // Add (POST)
        [HttpPost]
        public async Task<IActionResult> Add(Student student, Guid studentCourseID)
        {
            if (ModelState.IsValid)
            {
                if (student.studentCourses == null)
                {
                    student.studentCourses = new List<StudentCourse>();
                }

                var student_add = new Student
                {
                    Id = Guid.NewGuid(), 
                    Name = student.Name,
                    Email = student.Email,
                    Phone = student.Phone,
                    dateOfBirth = student.dateOfBirth,
                    className = student.className,
                    studentCourses = new List<StudentCourse>()
                };

                dbContext.Students.Add(student_add);
                await dbContext.SaveChangesAsync();

                var student_addcourse = new StudentCourse
                {
                    StudentId = student_add.Id,
                    CourseId = studentCourseID
                };

                student_add.studentCourses.Add(student_addcourse);
                dbContext.StudentCourses.Add(student_addcourse);

                await dbContext.SaveChangesAsync();
                return RedirectToAction("List", "Student");
            }

            foreach (var modelState in ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    Console.WriteLine($"Key: {modelState.Key}, Error: {error.ErrorMessage}");
                }
            }

            ViewBag.Courses = await dbContext.Courses.ToListAsync();
            return View(student);
        }


        // Edit (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var student = await dbContext.Students.FindAsync(Id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            var student_edit = await dbContext.Students.FindAsync(student.Id);
            if (student_edit != null)
            {
                student_edit.Name = student.Name;
                student_edit.Email = student.Email;
                student_edit.Phone = student.Phone;
                student_edit.dateOfBirth = student.dateOfBirth;
                student_edit.className = student.className;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Student");
        }

        // List of Students
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students
                .Include(s => s.studentCourses)
                .ThenInclude(sc => sc.course)
                .ToListAsync();
            return View(students);
        }

        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var student_delete = dbContext.Students.Find(Id);
            if (student_delete != null)
                dbContext.Students.Remove(student_delete);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Student");
        }
    }
}
