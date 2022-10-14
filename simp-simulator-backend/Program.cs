using simp_simulator_backend.Services;
using simp_simulator_models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<SimpSimulatorDatabaseSettings>(
    builder.Configuration.GetSection("SimpDatabase"));

builder.Services.AddSingleton<SimpsService>();
builder.Services.AddSingleton<JobsService>();

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
