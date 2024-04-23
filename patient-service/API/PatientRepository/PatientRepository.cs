using API;
using Domain;

namespace Repository;

public class PatientRepository : IPatientRepository
{
    public readonly DBContext _dbContext;

    public PatientRepository(DBContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task<API.PatientBe> GetPatient(int ssn)
    {
        return Task.FromResult(_dbContext.Patients.Find(ssn));
    }

    public void AddPatient(PatientBe patient)
    {
        _dbContext.Patients.Add(patient);
        _dbContext.SaveChanges();
    }

    public void UpdatePatient(PatientBe patient)
    {
        _dbContext.Patients.Update(patient);
        _dbContext.SaveChanges();	
    }

    public void DeletePatient(int ssn)
    {
        var patient = _dbContext.Patients.Find(ssn);
        _dbContext.Patients.Remove(patient);
        _dbContext.SaveChanges();
    }
}
