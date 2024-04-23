using API;
using Domain;

namespace API.PatientRepository;

public interface IPatientRepository
{
    Task<PatientBe> GetPatient(int ssn);
    void AddPatient(PatientBe patient);
    void UpdatePatient(PatientBe patient);
    void DeletePatient(int ssn);
}
