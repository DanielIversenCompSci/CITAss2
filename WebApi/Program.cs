using BusinessLayer;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

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

// 5. **Add Controllers**
builder.Services.AddControllers();

// 6. **Swagger Configuration**
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 7. **Middleware Configuration**
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 8. **Enable CORS Middleware**
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
