using BusinessLayer; 
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register IDataService and DataService in the DI container
builder.Services.AddScoped<IDataService, DataService>();

// Register your DbContext (ImdbContext) in the DI container
builder.Services.AddDbContext<ImdbContext>(options =>
    options.UseNpgsql("host=cit.ruc.dk;db=cit04;uid=cit04;pwd=1paLIXo0SHSs"));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();