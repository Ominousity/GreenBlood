using Domain;

namespace API.PatientService
{
    public interface IPatientService
    {
        Task<Patient> GetPatient(int ssn);
        void AddPatient(Patient patient);
        void DeletePatient(int ssn);
    }
}
