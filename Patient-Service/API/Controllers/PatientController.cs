using PatientService;
using Domain;
using Microsoft.AspNetCore.Mvc;

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
        Patient patient = _patientService.GetPatient(ssn).Result;
        return Ok(patient);
    }

    [HttpPost]
    [Route("add")]
    public IActionResult AddPatient(Patient patient)
    {
        _patientService.AddPatient(patient);
        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    public IActionResult DeletePatient(string ssn)
    {
        _patientService.DeletePatient(ssn);
        return Ok();
    }
}
