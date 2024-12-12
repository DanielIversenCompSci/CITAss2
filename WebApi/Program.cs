using BusinessLayer;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. **Get the Connection String**
var connectionString = builder.Configuration.GetSection("ConnectionString").Value;

// 2. **Register Services**
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddSingleton<AuthenticationHelper>();

// 3. **Register DbContext**
builder.Services.AddDbContext<ImdbContext>(options =>
    options.UseNpgsql(connectionString));

// 4. **Configure CORS Policy** for Development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Allow requests from any origin
              .AllowAnyMethod()   // Allow all HTTP methods
              .AllowAnyHeader();  // Allow all headers
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// 5. **JWT Configuration**
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// 6. **Add Controllers**
builder.Services.AddControllers();

// 7. **Swagger Configuration**
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 8. **Middleware Configuration**
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 9. **Enable CORS Middleware**
app.UseCors("AllowAll");

// 10. **Enable Authentication and Authorization Middleware**
app.UseHttpsRedirection();

app.UseAuthentication(); // Add this before Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
