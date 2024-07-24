using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data;
using StudentManagement.Models.Entity;

namespace StudentManagement.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CourseController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Course course)
        {
            var courses_add = new Course
            {
                name = course.name,
                description = course.description,
                dateStart = course.dateStart,
                dateEnd = course.dateEnd
            };

            await dbContext.Courses.AddAsync(courses_add);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Student");
        }
    }
}
