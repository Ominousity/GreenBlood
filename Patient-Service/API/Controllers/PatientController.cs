using PatientService;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{

    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    [Route("get")]
    public IActionResult GetPatient(string ssn)
    {
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
    public IActionResult AddPatient(Patient patient)
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
    public IActionResult DeletePatient(string ssn)
    {
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
