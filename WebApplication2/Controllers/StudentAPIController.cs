﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly MydbContext context;

        public StudentAPIController(MydbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await context.Students.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            Student student = await context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            return Ok(student);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Student>> UpdateStudent(Student updatedStudent, int id)
        {
            /* if (student.Id != id)
             {
                 return BadRequest();
             }
             context.Entry(student).State = EntityState.Modified;
             await context.SaveChangesAsync();

             return Ok(student);*/

            var existingStudent = await context.Students.FindAsync(id);

            if (existingStudent == null)
            {
                return NotFound(); // Return 404 if the student doesn't exist
            }

            // Manually update properties
            existingStudent.StudentName = updatedStudent.StudentName;
            existingStudent.StudentGender = updatedStudent.StudentGender;
            existingStudent.Age = updatedStudent.Age;
            existingStudent.Standard = updatedStudent.Standard;
            existingStudent.FatherName = updatedStudent.FatherName;

            await context.SaveChangesAsync(); // Save changes to the database

            return Ok(existingStudent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            Student student =  await context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            context.Students.Remove(student);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
