using BusinessLayer;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetSection("ConnectionString").Value;
// Register IDataService and DataService in the DI container
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddSingleton<AuthenticationHelper>();


// Register your DbContext (ImdbContext) in the DI container
builder.Services.AddDbContext<ImdbContext>(options =>
    options.UseNpgsql(connectionString));

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin() // Allow requests from any origin (frontend URL)
              .AllowAnyMethod() // Allow all HTTP methods (GET, POST, PUT, DELETE, etc.)
              .AllowAnyHeader(); // Allow all headers (e.g., Content-Type, Authorization)
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS middleware
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
