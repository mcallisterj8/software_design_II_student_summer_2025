using SchoolSystemCrud.Models;
using SchoolSystemCrud.Data;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystemCrud.Services;

public class CrudService {
    private readonly ApplicationDbContext _context;

    public CrudService(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<List<Student>> GetAllStudentsAsync() {
        return await _context.Students.ToListAsync();
    }

    public async Task<List<Student>> GetAllStudentsWithCoursesAsync() {
        // Using Include() to load related Courses data
        return await _context.Students
            .Include(s => s.Courses)
            .ToListAsync();
    }

    public async Task<List<Course>> GetAllCoursesWithStudentsAsync() {
        // Using Include() to load related Students data
        return await _context.Courses
        .Include(c => c.Students)
        .ToListAsync();
    }

    // Example: Get student by ID, with their courses
    public async Task<Student?> GetStudentByIdAsync(int id) {
        return await _context.Students
            .Include(s => s.Courses)
            .SingleOrDefaultAsync(s => s.Id == id);
    }

    // Get a student by full name (for testing inserts and updates)
    public async Task<Student?> GetStudentByNameAsync(string firstName, string lastName) {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.FirstName == firstName && s.LastName == lastName);
    }

    // Get all students in a course
    public async Task<List<Student>> GetStudentsInCourseAsync(int courseId) {
        var course = await _context.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        return course?.Students.ToList() ?? new List<Student>();
    }

}