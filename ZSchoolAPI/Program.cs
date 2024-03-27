using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ZSchoolAPI.Models;
using ZScool.Services;
using ZScool.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var urlFrontEnd = builder.Configuration.GetValue<string>("UrlFrontEnd");
//// Add services to the container.
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin",
//        builder =>
//        {
//            builder.WithOrigins(urlFrontEnd)
//                .AllowAnyHeader()
//                .AllowAnyMethod();
//        });
//});
builder.Services.AddCors(corsOptions => {
    corsOptions.AddPolicy("MyPolicy", corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ZSchoolDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ZSchoolDbConnection")));
builder.Services.AddIdentity<ApplactionUser,IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<ZSchoolDbContext>();
builder.Services.AddScoped<ISubjectServices, SubjectServices>();
builder.Services.AddScoped<IClassroomServices, ClassroomServices>();
builder.Services.AddScoped<IStudentServices, StudentServices>();
builder.Services.AddScoped<ISeanceServices, SeanceServices>();
builder.Services.AddScoped<ITeacherServices, TeacherServices>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudiance"],
        IssuerSigningKey =
        new SymmetricSecurityKey(GenerateSymmetricKey(builder.Configuration["JWT:Secret"]))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Teacher", policy => policy.RequireRole("Teacher"));
    options.AddPolicy("Student", policy => policy.RequireRole("Student"));
});
var app = builder.Build();
app.UseCors("MyPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


static byte[] GenerateSymmetricKey(string secret)
{
    // Convert the string secret to bytes using UTF-8 encoding
    byte[] keyBytes = Encoding.UTF8.GetBytes(secret);

    // Ensure the key is at least 256 bits (32 bytes) long
    if (keyBytes.Length < 32)
    {
        // If the key is too short, pad it with zeros or handle it as appropriate for your security requirements
        Array.Resize(ref keyBytes, 32);
    }

    return keyBytes;
}