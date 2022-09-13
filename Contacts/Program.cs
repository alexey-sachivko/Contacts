global using Contacts.Data;
global using Contacts.Models;
global using Contacts.Services.ContactService;
global using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:5002").AllowAnyHeader()
                                                  .AllowAnyMethod(); ;
                      });
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Contacts") ?? "Data Source=Contacts.db";

builder.Services.AddControllers();
builder.Services.AddSqlite<DataContext>(connectionString);

builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
