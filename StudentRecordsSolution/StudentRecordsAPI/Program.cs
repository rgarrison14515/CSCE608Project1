using Microsoft.EntityFrameworkCore;
using StudentRecordsAPI.Data;
using StudentRecordsAPI.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Prevent circular reference issues while keeping data structures clean
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext to use SQL Server
builder.Services.AddDbContext<StudentRecordsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<FacultyService>();
builder.Services.AddScoped<TermService>();
builder.Services.AddScoped<MajorService>();
builder.Services.AddScoped<StudentCourseService>();
builder.Services.AddScoped<StudentMajorService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
