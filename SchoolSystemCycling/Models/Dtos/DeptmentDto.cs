
namespace SchoolSystemCycling.Models.Dtos;

public class DepartmentDto {
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<InstructorDto> InstructorDtos { get; set; } = new List<InstructorDto>();
}