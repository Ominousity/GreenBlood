using AutoMapper;
using Domain;
using Measurments_Service.Be;
using Measurments_Service.Repository;
using Measurments_Service.Service;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add services to scope
builder.Services.AddScoped<IMeasurmentRepo, MeasurmentRepo>();
builder.Services.AddScoped<IMeasurmentService, MeasurmentService>();
builder.Services.AddDbContext<MeasurmentContext>();


//AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<MeasurmentBe, Measurement>();
    mc.CreateMap<Measurement, MeasurmentBe>();
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

// OpenTelemetry Config
builder.Services.AddOpenTelemetry().WithTracing(builder => builder
.AddAspNetCoreInstrumentation()
.AddSource("Measurment")
.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Measurment"))
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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

//creates the database if it doesn't exist or applies any pending migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MeasurmentContext>();
    if (context.Database.GetPendingMigrations().Any())
    {  context.Database.Migrate(); }
}

var MeasurmentGroup = app.MapGroup("/Measurment");

MeasurmentGroup.MapGet("/Get", async (IMeasurmentService measurmentService, string SSN) =>
{
    var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Measurment");
    using(var span = tracer.StartActiveSpan("Measurment")) 
    {
    span.SetAttribute("Measurment", SSN);
    Log.Logger.Information($"Got measurments from ssn = {SSN}");
    return await Task.FromResult(measurmentService.GetMeasurements(SSN));
    }
});

MeasurmentGroup.MapPost("/Add", async (IMeasurmentService measurmentService, Measurement measurement, string SSN) =>
{
    var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Measurment");
    using(var span = tracer.StartActiveSpan("Measurment")) 
    {
    span.SetAttribute("New Measurment", SSN);
    measurmentService.AddMeasurement(measurement, SSN);
    Log.Logger.Information($"Addead measurments to patient with SSN = {SSN}");
    return Results.Created($"/Measurment/Get?SSN={SSN}", measurement);
    }
});

MeasurmentGroup.MapPut("/Update", async (IMeasurmentService measurmentService, Measurement measurement, string SSN) =>
{
    var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Measurment");
    using(var span = tracer.StartActiveSpan("Measurment"))
    {
    span.SetAttribute("Update Measurment", SSN);
    measurmentService.UpdateMeasurement(measurement, SSN);
    Log.Logger.Information($"Updated Measurment for patient with SSN = {SSN}");
    return Results.NoContent();
    }
});

app.Run();


