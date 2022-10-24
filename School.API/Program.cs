using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using School.API.Services;
using SchoolDomain;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console().
            WriteTo.File("", rollingInterval: RollingInterval.Day).CreateBootstrapLogger();


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
// Add services to the container.

builder.Services.AddControllers(options =>
options.ReturnHttpNotAcceptable = true
).AddXmlDataContractSerializerFormatters()
.AddNewtonsoftJson();




//builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddDbContext<SchoolContext>(
    options => options.
    UseSqlServer(
    builder.Configuration["ConnectionStrings:SchoolConnectionString"]
    )
);
//the IstudentRepository should be Public 
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
//builder.Services.AddScoped<StudentRepository>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();

builder.Services.AddAuthentication("Bearer").AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "issuer",
            ValidAudience = "Audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MyAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAASecret"))
        };
    }
    );
builder.Services.AddAuthorization(
    options =>
    options.AddPolicy("ShouldBeUser", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("User");
    })
    );;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
