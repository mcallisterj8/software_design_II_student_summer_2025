using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using SchoolSystemCycling.Data;
using SchoolSystemCycling.Models.Dtos;
using SchoolSystemCycling.Models.Entities;
using SchoolSystemCycling.Services;

ServiceProvider _serviceProvider;
SeedingService _seedingService;
BasicQueryService _basicQueryService;

// Create container to hold services for dependency injection
var services = new ServiceCollection();

// Add services to service container
services.AddDbContext<ApplicationDbContext>();
services.AddScoped<SeedingService>();
services.AddScoped<BasicQueryService>();

/*
    Get the service provider - this is our way to take something
    out of the container.
*/
_serviceProvider = services.BuildServiceProvider();

// Retrieve instance of SeedingService from the container
_seedingService = _serviceProvider.GetRequiredService<SeedingService>();

// Retrieve instance of BasicQueryService from the container
_basicQueryService = _serviceProvider.GetRequiredService<BasicQueryService>();

// Call method to seed the database.
await _seedingService.SeedDatabase();

JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };

Console.WriteLine("\n=========== GetInstructorByIdWithDept() =============\n");

// Instructor? instructorWithDept = await _basicQueryService
//     .GetInstructorByIdWithDept(3);

// Console.WriteLine($"1) {instructorWithDept}");

// Console.WriteLine($"\n2) Instructor: {instructorWithDept.LastName} in Dept: {instructorWithDept.Department.Name}");

// Console.WriteLine($"\n3) {instructorWithDept.Department}");

// Console.WriteLine($"\n4) Instr Obj Serialized {JsonSerializer.Serialize(instructorWithDept, jsonOptions)}");

Console.WriteLine("=================================================================");
Console.WriteLine("=================================================================");

InstructorDto? instrDto = await _basicQueryService.GetInstructorDtoByIdWithDept(3);

Console.WriteLine($"Instructor DTO Serialized:\n{JsonSerializer.Serialize(instrDto, jsonOptions)}");
