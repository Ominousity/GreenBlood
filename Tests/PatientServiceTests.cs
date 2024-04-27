using AutoMapper;
using Domain;
using Newtonsoft.Json;
using NSubstitute;
using Patient_Repo;
using PatientService;
using System.Net;

namespace Tests;

public class UnitTest1
{
    private readonly IMapper _mapper;
    private readonly IPatientRepository _patientRepository;
    private readonly HttpClient _httpClient;
    private readonly PatientService.PatientService _patientService;

    public UnitTest1()
    {
        _mapper = Substitute.For<IMapper>();
        _patientRepository = Substitute.For<IPatientRepository>();
        _httpClient = Substitute.For<HttpClient>(new HttpClientHandler());

        _patientService = new PatientService.PatientService(_mapper, _patientRepository, _httpClient);
    }

    [Fact]
    public void DeletePatient_ShouldCallDeletePatientOnRepository()
    {
        // Arrange
        var ssn = "123-45-6789";

        // Act
        _patientService.DeletePatient(ssn);

        // Assert
        _patientRepository.Received(1).DeletePatient(ssn);
    }
}