using DepotService.StorageAccess;
using MissionPlanning.Api;
using MissionPlanningService.StorageAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<HttpClient>();
builder.Services.AddSingleton<MissionRepository, MemoryMissionRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();

