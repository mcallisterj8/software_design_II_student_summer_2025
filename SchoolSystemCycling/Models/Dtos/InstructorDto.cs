namespace SchoolSystemCycling.Models.Dtos;

public class InstructorDto {
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateTime JoiningDate { get; set; }

    public DepartmentDto? Department { get; set; }

}