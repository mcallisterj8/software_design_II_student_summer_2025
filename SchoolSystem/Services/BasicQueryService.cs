

using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Models;

namespace SchoolSystem.Services;

public class BasicQueryService {

    private readonly ApplicationDbContext _context;

    public BasicQueryService(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<List<string>> GetAllInstructorNames() {
        /*
            SELECT FirstName
            FROM Instructors
        */
        return await _context.Instructors
            .Select(instructor => instructor.FirstName)
            .ToListAsync();
    }

    public async Task<Instructor?> GetInstructorById(int instructorId) {
        // return await _context.Instructors.FindAsync(instructorId);

        return await _context.Instructors
            .Include(instr => instr.Department)
            .SingleOrDefaultAsync(instr => instr.Id == instructorId);
    }

    public async Task<List<string>> GetDeptNamesWithMoreThanOneCourse() {
        /*
            SELECT Name
            FROM Departments
            WHERE Departments.Courses.Count > 1
        */

        return await _context.Departments
            .Where(dept => dept.Courses.Count > 1)
            .Select(dept => dept.Name)
            .ToListAsync();
    }

    public async Task<string?> GetDeptWithMostCourses() {
        /*
            SELECT Name
            FROM Departments
            ORDER BY Departments.Courses.Count DESC
            LIMIT 1

        */
        return await _context.Departments
            .OrderByDescending(dept => dept.Courses.Count)
            .Select(dept => dept.Name)
            .FirstOrDefaultAsync();

    }



}
