using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{

    private readonly IPatientRepository _patientRepository;

    public PatientController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    

    [HttpGet]
    [Route("get/{ssn}")]
    public IActionResult GetPatient(int ssn)
    {
        Patient patient = _patientRepository.GetPatient(ssn).Result;
        return Ok(patient);
    }

    [HttpPost]
    [Route("add")]
    public IActionResult AddPatient(Patient patient)
    {
        _patientRepository.AddPatient(patient);
        return Ok();
    }

    [HttpPut]
    [Route("update")]
    public IActionResult UpdatePatient(Patient patient)
    {
        _patientRepository.UpdatePatient(patient);
        return Ok();
    }

    [HttpDelete]
    [Route("delete/{ssn}")]
    public IActionResult DeletePatient(int ssn)
    {
        _patientRepository.DeletePatient(ssn);
        return Ok();
    }

    [HttpPost]
    [Route("rebuild")]
    public IActionResult Rebuild()
    {
        _patientRepository.Rebuild();
        return Ok();
    }
}
