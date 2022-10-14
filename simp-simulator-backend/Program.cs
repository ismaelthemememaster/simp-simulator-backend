using Microsoft.OpenApi.Models;
using simp_simulator_backend.Services;
using simp_simulator_models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<SimpSimulatorDatabaseSettings>(
    builder.Configuration.GetSection("SimpDatabase"));

builder.Services.AddSingleton<SimpsService>();
builder.Services.AddSingleton<JobsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "SimpSimulator",
        Description = "A joke proyect to make fun of simps and practice .net and c#",
        TermsOfService = new Uri("https://youtu.be/L7OXv_BXN5Y"),
    });
    // Set Swagger to use xml comments on the gui.
    // Follow the previous link to get rid of CS1591 warnings (For now I'll leave them as a reminder to add the xml comments)
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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
