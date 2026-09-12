using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;
using Nexora.Api.Models;

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
    public async Task<ActionResult<List<Course>>> GetAll()
    {
        var courses = await _context.Courses
            .Include(c => c.Teacher)
            .ToListAsync();

        return Ok(courses);
    }

    // GET api/courses/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Course>> GetById(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Teacher)
            .FirstOrDefaultAsync(c => c.Id == id);

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
    public async Task<ActionResult<Course>> Create(Course course)
    {
        var teacherExists = await _context.Teachers
            .AnyAsync(t => t.Id == course.TeacherId);

        if (!teacherExists)
        {
            return BadRequest("Teacher does not exist.");
        }

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = course.Id },
            course);
    }

    // PUT api/courses/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Course input)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        course.Title = input.Title;
        course.TeacherId = input.TeacherId;

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