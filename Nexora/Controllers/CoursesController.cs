using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;
using Nexora.Api.Models;
using Nexora.Api.Dtos.Courses;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly NexoraDbContext _context;

    public CoursesController(NexoraDbContext context)
    {
        _context = context;
    }

    // GET api/courses
    [HttpGet]
    public async Task<ActionResult<List<CourseDto>>> GetAll()
    {
        var courses = await _context.Courses
            .Include(c => c.Teacher)
            .Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                TeacherId = c.TeacherId,
                TeacherName = c.Teacher.Name
            })  
            .ToListAsync();

        return Ok(courses);
    }

    // GET api/courses/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CourseDto>> GetById(int id)
    {
        var course = await _context.Courses
            .Where(c => c.Id == id)
            .Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                TeacherId = c.TeacherId,
                TeacherName = c.Teacher.Name
            })
            .FirstOrDefaultAsync();

        if (course is null)
        {
            return NotFound();
        }

        return Ok(course);
    }

    // GET api/courses/exists/1
    [HttpGet("exists/{id:int}")]
    public async Task<ActionResult<bool>> Exists(int id)
    {
        var exists = await _context.Courses
            .AnyAsync(c => c.Id == id);

        return Ok(exists);
    }

    // GET api/courses/count
    [HttpGet("count")]
    public async Task<ActionResult<int>> Count()
    {
        var count = await _context.Courses.CountAsync();

        return Ok(count);
    }

    // POST api/courses
    [HttpPost]
    public async Task<ActionResult<CourseDto>> Create(CreateCourseDto dto)
    {
        var teacher = await _context.Teachers.FindAsync(dto.TeacherId);

        if (teacher is null)
        {
            return BadRequest("Teacher does not exist.");
        }
        
        var course = new Course
        {
            Title = dto.Title,
            TeacherId = dto.TeacherId
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        var result = new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            TeacherId = course.TeacherId,
            TeacherName = teacher.Name
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = course.Id },
            result);
    }

    // PUT api/courses/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCourseDto dto)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course is null)
        {
            return NotFound();
        }

        var teacher = await _context.Teachers.FindAsync(dto.TeacherId);
        if (teacher is null)
        {
            return BadRequest("Teacher does not exist.");
        }

        course.Title = dto.Title;
        course.TeacherId = dto.TeacherId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/courses/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}