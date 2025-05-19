

using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Models;
using SchoolSystem.Models.Dtos;

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
        // return await _context.Instructors.FindAsync(instructorId); // return await _context.Instructors.FindAsync(instructorId); // This is EF Core Find() - different than regular LINQ Find()

        return await _context.Instructors
            .Include(instr => instr.Department)            
            .SingleOrDefaultAsync(instr => instr.Id == instructorId);
    }

    public async Task<object?> GetInstructorObjectById(int instructorId) {
        // return await _context.Instructors.FindAsync(instructorId); // return await _context.Instructors.FindAsync(instructorId); // This is EF Core Find() - different than regular LINQ Find()

        return await _context.Instructors
            .Where(instr => instr.Id == instructorId)
            .Include(instr => instr.Department)
            .Select(instr => new { 
                LastName = instr.LastName,
                DepartmentName = instr.Department.Name
            })            
            .SingleOrDefaultAsync();
    }

    public async Task<InstructorDto?> GetInstructorDtoById(int instructorId) {
        // return await _context.Instructors.FindAsync(instructorId); // return await _context.Instructors.FindAsync(instructorId); // This is EF Core Find() - different than regular LINQ Find()

        return await _context.Instructors
            .Where(instr => instr.Id == instructorId)
            .Include(instr => instr.Department)
            .Select(instr => new InstructorDto { 
                LastName = instr.LastName,
                DepartmentName = instr.Department.Name
            })            
            .SingleOrDefaultAsync();
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
