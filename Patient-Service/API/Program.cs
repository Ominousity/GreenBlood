using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Patient_Repo;
using PatientService;

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
builder.Services.AddDbContext<DBContext>();

builder.Services.AddControllers();

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