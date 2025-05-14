
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SchoolSystem.Data;
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

List<string> instrNames = 
    await _basicQueryService.GetAllInstructorNames();

foreach(string name in instrNames) {
    Console.WriteLine($"Name: {name}");
}
