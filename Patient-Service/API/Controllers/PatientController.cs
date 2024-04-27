using PatientService;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using FeatureHubSDK;
using API.Controllers;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Runtime;
using IPinfo;
using Newtonsoft.Json.Linq;
using IPinfo.Models;


namespace API;


[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{

    private readonly IPatientService _patientService;
    private readonly IFeatureToggle _featureToggle;
    private readonly IPinfoClient _ipinfoClient;


    public PatientController(IPatientService patientService, IFeatureToggle featureToggle)
    {
        _patientService = patientService;
        _featureToggle = featureToggle;
        _ipinfoClient = new IPinfoClient.Builder()
    .AccessToken("5b598021b83b0a")
    .Build();

    }
    public async Task<string> GetUserCountryByIp(string ip)
    {
       IPResponse ipResponse = await _ipinfoClient.IPApi.GetDetailsAsync(ip);
       return ipResponse.Country;
    }


        [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetPatient(string ssn)
    {
        var feature = await _featureToggle.CanDoctorGet();
        if (!feature)
        {
            Log.Logger.Information("Get Patient is disabled");
            return BadRequest("This Feature is disabled");
        }
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var countryIP = await GetUserCountryByIp(ipAddress);
        Log.Logger.Information($"I AM FROM :{ipAddress}");
        var featureCountry = await _featureToggle.IsFromDenmark(countryIP);
        if (!featureCountry)
        {
            Log.Logger.Information("You Are Not from Denmark");
            return BadRequest("You Are Not from Denmark");
        }

        var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("API");
        using(var span = tracer.StartActiveSpan("API")) 
        {
        span.SetAttribute("Patient", ssn);
        Patient patient = _patientService.GetPatient(ssn).Result;
        Log.Logger.Information($"got the information on patient from ssn = {ssn}");
        return Ok(patient);
        }
    }

    [HttpPost]
    [Route("add")]
    public  IActionResult AddPatient(Patient patient)
    {
        var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("API");
        using(var span = tracer.StartActiveSpan("API")) 
        {
        span.SetAttribute("New Patient", patient.Id);
        _patientService.AddPatient(patient);
        Log.Logger.Information($"Patient created: {patient.Id}");
        return Ok();
        }
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeletePatient(string ssn)
    {
        var feature = await _featureToggle.CanDoctorDelete();
        if (!feature)
        {
            Log.Logger.Information("Get Patient is disabled");
            return BadRequest("This Feature is disabled");
        }

        var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("API");
        using(var span = tracer.StartActiveSpan("API")) 
        {
        span.SetAttribute("Deleted Patient", ssn);
        await _patientService.DeletePatient(ssn);
        Log.Logger.Information($"Deleted Patient with SSN = {ssn}");
        return Ok();
        }
    }
}
