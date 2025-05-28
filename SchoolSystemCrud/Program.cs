using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using SchoolSystemCrud.Data;
using SchoolSystemCrud.Models;
using SchoolSystemCrud.Services;

ServiceProvider _serviceProvider;
SeedingService _seedingService;
CrudService _crudService;


// Create container to hold services for dependency injection
var services = new ServiceCollection();

// Add services to service container
services.AddDbContext<ApplicationDbContext>();
services.AddScoped<SeedingService>();
services.AddScoped<CrudService>();

/*
    Get the service provider - this is our way to take something
    out of the container.
*/
_serviceProvider = services.BuildServiceProvider();

// Retrieve instance of SeedingService from the container
_seedingService = _serviceProvider.GetRequiredService<SeedingService>();

_crudService = _serviceProvider.GetRequiredService<CrudService>();

await _seedingService.SeedDatabase();