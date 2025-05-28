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

    public async Task<Student?> GetStudentByIdAsync(int id) {
        return await _context.Students
            .Include(s => s.Courses)
            .SingleOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Student?> GetStudentByNameAsync(string firstName, string lastName) {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.FirstName == firstName && s.LastName == lastName);
    }

    public async Task<List<Student>> GetStudentsInCourseAsync(int courseId) {
        var course = await _context.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        return course?.Students.ToList() ?? new List<Student>();
    }

    /************************************************************
    *********************** CRUD METHODS ************************
    ************************************************************/
    public async Task<bool> AddStudentAsync(Student student) {
        if (null == student) {
            return false;
        }

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Student?> UpdateStudentAsync(Student studentDetails) {
        var student = await _context.Students.FindAsync(studentDetails.Id);

        if (null != student) {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
        return student;
    }

    public async Task<Student?> UpdateStudentNameAsync(int studentId, string newFirstName, string newLastName) {
        var student = await _context.Students.FindAsync(studentId);

        if (null != student) {
            student.FirstName = newFirstName;
            student.LastName = newLastName;

            await _context.SaveChangesAsync();
        }

        return student;
    }

    public async Task<bool> EnrollStudentInCourseAsync(int studentId, int courseId) {
        var student = await _context.Students.FindAsync(studentId);

        if (null == student) {
            return false;
        }

        var course = await _context.Courses
            .Include(c => c.Students)
            .SingleOrDefaultAsync(c => c.Id == courseId);

        if (null == course) {
            return false;
        }

        course.Students.Add(student);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteStudent(int id) {
        bool result = false;
        var student = await _context.Students.FindAsync(id);

        if (null != student) {
            _context.Students.Remove(student);

            await _context.SaveChangesAsync();
            result = true;
        }

        return result;
    }
}