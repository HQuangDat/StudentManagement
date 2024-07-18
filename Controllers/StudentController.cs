﻿using Microsoft.AspNetCore.Mvc;
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


        //Add 
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

        //Edit
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
            if(student_edit != null)
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

        //List of Students
        [HttpGet]
        public async Task<IActionResult> List() 
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }

        //Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id) 
        {
            var student_delete =  dbContext.Students.Find(Id);
            if(student_delete != null)
                dbContext.Students.Remove(student_delete);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Student");
        }
    }
}
