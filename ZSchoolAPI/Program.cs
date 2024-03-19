using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZSchoolAPI.Models;
using ZScool.Services;
using ZScool.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var urlFrontEnd = builder.Configuration.GetValue<string>("UrlFrontEnd");
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins(urlFrontEnd)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ZSchoolDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ZSchoolDbConnection")));
builder.Services.AddIdentity<ApplactionUser,IdentityRole>().AddEntityFrameworkStores<ZSchoolDbContext>();
builder.Services.AddScoped<ISubjectServices, SubjectServices>();
builder.Services.AddScoped<IClassroomServices, ClassroomServices>();
builder.Services.AddScoped<IStudentServices, StudentServices>();
builder.Services.AddScoped<ISeanceServices, SeanceServices>();
builder.Services.AddScoped<ITeacherServices, TeacherServices>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
