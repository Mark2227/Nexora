using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;
using Nexora.Api.Models;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly NexoraDbContext _context;

    public StudentsController(NexoraDbContext context)
    {
        _context = context;
    }

    // GET api/students
    [HttpGet]
    public async Task<ActionResult<List<Student>>> GetAll()
    {
        var students = await _context.Students.ToListAsync();
        return Ok(students);
    }

    // GET api/students/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Student>> GetById(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student is null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    // GET api/students/exists/1
    [HttpGet("exists/{id:int}")]
    public async Task<ActionResult<bool>> Exists(int id)
    {
        var exists = await _context.Students.AnyAsync(s => s.Id == id);
        return Ok(exists);
    }

    // GET api/students/count
    [HttpGet("count")]
    public async Task<ActionResult<int>> Count()
    {
        var count = await _context.Students.CountAsync();
        return Ok(count);
    }

    // POST api/students
    [HttpPost]
    public async Task<ActionResult<Student>> Create(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }

    // PUT api/students/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Student input)
    {
        var student = await _context.Students.FindAsync(id);

        if (student is null)
        {
            return NotFound();
        }

        student.Name = input.Name;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/students/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student is null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}