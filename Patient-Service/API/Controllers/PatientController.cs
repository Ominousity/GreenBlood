using PatientService;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using FeatureHubSDK;
using API.Controllers;


namespace API;


[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{

    private readonly IPatientService _patientService;
    private readonly IFeatureToggle _featureToggle;


    public PatientController(IPatientService patientService, IFeatureToggle featureToggle)
    {
        _patientService = patientService;
        _featureToggle = featureToggle;

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
        _patientService.DeletePatient(ssn);
        Log.Logger.Information($"Deleted Patient with SSN = {ssn}");
        return Ok();
        }
    }
}
