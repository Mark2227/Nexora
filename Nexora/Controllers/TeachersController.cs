using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;
using Nexora.Api.Models;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly NexoraDbContext _context;
    public TeachersController(NexoraDbContext context)
    {
        _context = context;
    }

    // GET api/teachers
    [HttpGet]
    public async Task<ActionResult<List<Teacher>>> GetAll()
    {
        var teachers = await _context.Teachers.ToListAsync();
        return Ok(teachers);
    }

    // GET api/teachers/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Teacher>> GetById(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher is null)
        {
            return NotFound();
        }
        return Ok(teacher);
    }

    // GET api/teachers/exists/1
    [HttpGet("exists/{id:int}")]
    public async Task<ActionResult<bool>> Exists(int id)
    {
        var exists = await _context.Teachers.AnyAsync(t => t.Id == id);
        return Ok(exists);
    }

    // GET api/teachers/count
    [HttpGet("count")]
    public async Task<ActionResult<int>> Count()
    {
        var count = await _context.Teachers.CountAsync();
        return Ok(count);
    }

    // POST api/teachers
    [HttpPost]
    public async Task<ActionResult<Teacher>> Create(Teacher teacher)
    {
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, teacher);
    }

    // PUT api/teachers/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Teacher teacher)
    {
        var existingTeacher = await _context.Teachers.FindAsync(id);
        if (existingTeacher is null)
        {
            return NotFound();
        }
        existingTeacher.Name = teacher.Name;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/teachers/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher is null)
        {
            return NotFound();
        }
        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
