using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient_Repo
{
    public interface IPatientRepository
    {
        Task<PatientBe> GetPatient(int ssn);
        void AddPatient(PatientBe patient);
        void UpdatePatient(PatientBe patient);
        void DeletePatient(int ssn);
    }
}
