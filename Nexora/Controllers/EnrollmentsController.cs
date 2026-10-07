using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;
using Nexora.Api.Models;
using Nexora.Api.Dtos.Enrollments;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly NexoraDbContext _context;
    public EnrollmentsController(NexoraDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Enrollment>>> GetAll()
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .ToListAsync();
    }

    [HttpGet("student/{studentId:int}")]
    public async Task<ActionResult<List<Enrollment>>> GetByStudent(int studentId)
    {
        var studentExists = await _context.Students.AnyAsync(s => s.Id == studentId);
        if (!studentExists)
        {
            return NotFound("Student does not exist.");
        }

        var enrollments = await _context.Enrollments
            .Where(e => e.StudentId == studentId)
            .Include(e => e.Course)
            .ToListAsync();

        return Ok(enrollments);
    }

    [HttpGet("course/{courseId:int}")]
    public async Task<ActionResult<List<Enrollment>>> GetByCourse(int courseId)
    {
        var courseExists = await _context.Courses.AnyAsync(c => c.Id == courseId);
        if (!courseExists)
        {
            return NotFound("Course does not exist.");
        }
        var enrollments = await _context.Enrollments
            .Where(e => e.CourseId == courseId)
            .Include(e => e.Student)
            .ToListAsync();
        return Ok(enrollments);
    }

    [HttpPost]
    public async Task<IActionResult> Enroll(CreateEnrollmentDto dto)
    {
        var studentExists = await _context.Students.AnyAsync(s => s.Id == dto.StudentId);
        var courseExists = await _context.Courses.AnyAsync(c => c.Id == dto.CourseId);
        if (!studentExists)
        {
            return BadRequest("Student does not exist.");
        }
        if (!courseExists)
        {
            return BadRequest("Course does not exist.");
        }
        var alreadyEnrolled = await _context.Enrollments
            .AnyAsync(e => e.StudentId == dto.StudentId && e.CourseId == dto.CourseId);
        if (alreadyEnrolled)
        {
            return Conflict("Student is already enrolled in this course.");
        }
        var enrollment = new Enrollment
        {
            StudentId = dto.StudentId,
            CourseId = dto.CourseId
        };
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetByStudent), new { studentId = enrollment.StudentId }, enrollment);
    }

    [HttpDelete("student/{studentId:int}/course/{courseId:int}")]
    public async Task<IActionResult> Unenroll(int studentId, int courseId)
    {
        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);

        if (enrollment is null)
        {
            return NotFound();
        }

        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
