using Microsoft.EntityFrameworkCore;
using usersapp;
using usersapp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb") ?? throw new InvalidOperationException("Connection string 'UserContext' not found.")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(
        options => options.WithOrigins("https://localhost:5002").AllowAnyMethod().AllowAnyHeader()
    );

app.MapControllers();
app.Run();
