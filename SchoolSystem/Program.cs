
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SchoolSystem.Data;
using SchoolSystem.Models;
using SchoolSystem.Services;

ServiceProvider _serviceProvider;
SeedingService _seedingService;
BasicQueryService _basicQueryService;

// Create container to hold services for dependency injection
var services = new ServiceCollection();

// Add services to the service container
services.AddDbContext<ApplicationDbContext>();
services.AddScoped<SeedingService>();
services.AddScoped<BasicQueryService>();

/*
    Get the service provider - this our way to take
    something out of the container.
*/
_serviceProvider = services.BuildServiceProvider();

// Retrieve instance of SeedingService from the container
_seedingService = _serviceProvider.GetRequiredService<SeedingService>();
_basicQueryService = _serviceProvider.GetRequiredService<BasicQueryService>();

// Call method to seed the database
await _seedingService.SeedDatabase();

Instructor? instructor = await _basicQueryService.GetInstructorById(3);

Console.WriteLine($"\nDept: {instructor.Department.Name}\n");

// var jsonOptions = new JsonSerializerOptions { WriteIndented = true };

// Console.WriteLine(JsonSerializer.Serialize(instructor, jsonOptions));

// Console.WriteLine("\n============== GetInstructorObjectById ====================");

// var instrObj = await _basicQueryService.GetInstructorObjectById(3);
// Console.WriteLine(JsonSerializer.Serialize(instrObj, jsonOptions));

// Console.WriteLine("\n============== GetInstructorDtoById ====================");

// var instrDto = await _basicQueryService.GetInstructorDtoById(3);
// Console.WriteLine(JsonSerializer.Serialize(instrObj, jsonOptions));
