using Attendance_Manager.API.Controllers.Class;
using Attendance_Manager.API.Controllers.Session;
using Attendance_Manager.API.Controllers.User;
using Attendance_Manager.API.Data;
using Attendance_Manager.API.JwtAuthentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
    {
        c.SwaggerDoc("v2", new OpenApiInfo
        {
            Title = "my api",
            Version = "v2"
        });
    }
);

builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AttendanceRepository>();
builder.Services.AddScoped<ClassService>();
builder.Services.AddScoped<SessionService>();

// Configure EF Core
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDBContext>()
    .AddDefaultTokenProviders();

// Configure Jwt Authentication
var jwt = builder.Configuration.GetSection("Jwt");
var key = System.Text.Encoding.UTF8.GetBytes(jwt["Key"]);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

var app = builder.Build();

// Seed Roles and Default Admin User
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var dbContext = services.GetRequiredService<AppDBContext>();

    //dbContext.Database.Migrate(); 

    await SeedData.InitializeAsync(roleManager, userManager,dbContext);
}


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger(c =>
        {
            c.SerializeAsV2 = true; // Forces Swagger 2.0 if needed
        });
        app.UseSwaggerUI(c =>
        {
            // Point UI to the correct JSON
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
        });

    }

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
