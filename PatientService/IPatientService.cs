using Domain;

namespace PatientService
{
    public interface IPatientService
    {
        Task<Patient> GetPatient(string ssn);
        void AddPatient(Patient patient);
        Task DeletePatient(string ssn);
    }
}
