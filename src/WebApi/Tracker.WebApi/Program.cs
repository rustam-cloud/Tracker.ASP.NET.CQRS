using Microsoft.OpenApi.Models;
using Tracker.Application;
using Tracker.Application.Common.Interfaces;
using Tracker.Infrastructure;
using Tracker.Infrastructure.Dal;
using Tracker.Shared;

var builder = WebApplication.CreateBuilder(args).AddShared();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddResponseCaching(x =>
{
    x.MaximumBodySize = 1024;
    x.UseCaseSensitivePaths = true;
});
builder.Services.AddApplication();
builder.Services.AddScoped<ITrackerDBContext, TrackerDbContext>();
builder.Services.AddDataBaseContext(connectionString ?? "");

builder.Services.AddEndpointsApiExplorer();
// convert DateOnly to string
builder.Services.AddSwaggerGen(c => c.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" }));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//add cashing
app.UseResponseCaching();
app.UseShared();
app.MapControllers();

app.Run();
