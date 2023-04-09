using Tracker.Application;
using Tracker.Application.Common.Interfaces;
using Tracker.Infrastructure;
using Tracker.Infrastructure.Dal;
using Tracker.Shared;

var builder = WebApplication.CreateBuilder(args).AddShared();

var connectionString = builder.Configuration.GetConnectionString("postgresConnection");
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddScoped<ITrackerDBContext, TrackerDbContext>();
builder.Services.AddDataBaseContext(connectionString ?? "");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//add cashing
app.UseResponseCaching();
app.UseShared();

app.Run();
