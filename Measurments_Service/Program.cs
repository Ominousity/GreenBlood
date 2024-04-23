using AutoMapper;
using Domain;
using Measurments_Service.Be;
using Measurments_Service.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add services to scope
builder.Services.AddScoped<IMeasurmentRepo, MeasurmentRepo>();

//AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<MeasurmentBe, Measurement>();
    mc.CreateMap<Measurement, MeasurmentBe>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//creates the database if it doesn't exist or applies any pending migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MeasurmentContext>();
    if (context.Database.GetPendingMigrations().Any())
    {  context.Database.Migrate(); }
}

app.Run();

var MeasurmentsApi = app.MapGroup("/MeasurmentsApi");
