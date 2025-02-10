using gg_test.Data;
using gg_test.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

// Add MySQL Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions
            .EnableRetryOnFailure()
    ));

// Add controllers
builder.Services.AddControllers();

// Add services
builder.Services.AddScoped<EmailService>();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "gg-test API", Version = "v1" });
});

var smtpSettings = builder.Configuration.GetSection("SmtpSettings");

string smtpUser = Environment.GetEnvironmentVariable("SMTP_USERNAME") ?? smtpSettings["Username"];
string smtpPass = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? smtpSettings["Password"];

builder.Services.Configure<SmtpSettings>(options =>
{
    options.Host = smtpSettings["Host"];
    options.Port = int.Parse(smtpSettings["Port"]);
    options.Username = smtpUser;
    options.Password = smtpPass;
    options.EnableSsl = bool.Parse(smtpSettings["EnableSsl"]);
    options.FromEmail = smtpSettings["FromEmail"];
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // This will serve the Swagger UI at the root URL
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();
