using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NexusMonitor.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// CORS Policy to allow requests from the React app running on Vite

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Rejestracja FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters(); // Opcjonalne (dla frontendu)
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Rejestracja AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Obs³uga bazy danych
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidation();


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors("AllowReactApp");

app.UseMiddleware<NexusMonitor.Api.Middleware.ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
