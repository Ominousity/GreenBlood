using API.Controllers;
using AutoMapper;
using Domain;
using FeatureHubSDK;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Patient_Repo;
using PatientService;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add automapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<PatientBe, Patient>();
    mc.CreateMap<Patient, PatientBe>();
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService.PatientService>();
builder.Services.AddScoped<IFeatureToggle,  FeatureToggle>();
builder.Services.AddDbContext<DBContext>();

builder.Services.AddControllers();

// OpenTelemetry Config
builder.Services.AddOpenTelemetry().WithTracing(builder => builder
.AddAspNetCoreInstrumentation()
.AddSource("API")
.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("API"))
.AddZipkinExporter(options =>
{
    options.Endpoint = new Uri("http://zipkin:9411/api/v2/spans");
}));


// Configures the logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentUserName()
    .WriteTo.Seq("http://seq:5341")
    .WriteTo.Console()
    .CreateLogger();


builder.Host.UseSerilog();

var app = builder.Build();

//creates the database if it doesn't exist or applies any pending migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DBContext>();
    if (context.Database.GetPendingMigrations().Any())
    { context.Database.Migrate(); }

}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.MapControllers();

app.Run();