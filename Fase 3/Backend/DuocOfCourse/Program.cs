using System.Text;
using System.Text.Json.Serialization;
using Api.Core.IRepositories;
using Api.Core.IServices;
using Api.Repository;
using Api.Repository.Repositories;
using Api.Service.Service;
using Api.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// Swagger / utilidades
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

// 🔹 CORS: política con nombre
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// 🔹 Auth (JWT)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });

// 🔹 Repositorios
builder.Services.AddScoped<ISchoolsRepository, SchoolsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IPasswordResetRepository, PasswordResetRepository>();
builder.Services.AddScoped<IAuthCredentialsRepository, AuthCredentialsRepository>();
builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
builder.Services.AddScoped<IEvaluationRepository, EvaluationsRepository>();
builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();

// 🔹 Servicios
builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<IEvaluationService, EvaluationService>();

builder.Services.AddScoped<IGradesReportService, GradesReportService>();

builder.Services.AddSingleton<IGcpService, GcsStorageService>();

builder.Services.AddScoped<IAttemptService, AttemptService>();
builder.Services.AddScoped<IAttemptRepository, AttemptRepository>();

// 🔹 DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DuocOfCourseDB"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DuocOfCourseDB"))
    )
);

// 🔹 Controllers + JSON
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// =======================
//   Pipeline de la app
// =======================
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 👇 IMPORTANTE: orden del pipeline
app.UseHttpsRedirection();

app.UseStaticFiles();          // para servir wwwroot/media/...

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
