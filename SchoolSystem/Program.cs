﻿
using Microsoft.Extensions.DependencyInjection;
using SchoolSystem.Data;

ServiceProvider _serviceProvider;

// Create container to hold services for dependency injection
var services = new ServiceCollection();

// Add services to the service container
services.AddDbContext<ApplicationDbContext>();

/*
    Get the service provider - this our way to take
    something out of the container.
*/
_serviceProvider = services.BuildServiceProvider();

