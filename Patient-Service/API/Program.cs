using AutoMapper;
using Domain;
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();