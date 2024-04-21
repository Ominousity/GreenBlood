using Domain;

namespace Repository;

public interface IPatientRepository
{
    Task<Patient> GetPatient(int ssn);
    void AddPatient(Patient patient);
    void UpdatePatient(Patient patient);
    void DeletePatient(int ssn);

    void Rebuild();
}
