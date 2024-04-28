using AutoMapper;
using Domain;
using Measurments_Service.Be;
using Measurments_Service.Repository;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Measurments_Service.Service
{
    public class MeasurmentService : IMeasurmentService
    {
        public IMapper _mapper { get; set; }
        private readonly IMeasurmentRepo _Repo;
        private HttpClient _HttpClient;
        public MeasurmentService(IMapper mapper, IMeasurmentRepo measurmentRepo)
        {
            _mapper = mapper;
            _Repo = measurmentRepo;
            _HttpClient = new HttpClient { BaseAddress = new Uri("http://patient-service:8080") };
        }
        public void AddMeasurement(Measurement measurement, string SSN)
        {
            try
            {
                if (ValidateSSN(SSN))
                {
                    //map the measurement to the measurement entity
                    var measurementEntity = _mapper.Map<MeasurmentBe>(measurement);
                    //add the measurement to the database
                    measurementEntity.PatientSSN = SSN;
                    _Repo.AddMeasurement(measurementEntity);
                }
                else
                {
                    throw new Exception("Patient does not exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error in adding measurement", e);
            }
        }

        public List<Measurement> GetMeasurements(string SSn)
        {
            try
            {
                //get the measurements from the database
                var measurements = _Repo.GetMeasurements(SSn);
                //map the measurements to the measurement entity
                var measurementsEntity = _mapper.Map<List<Measurement>>(measurements);
                return measurementsEntity;
            }
            catch (Exception e)
            {
                throw new Exception("Error in getting measurements", e);
            }
        }

        public void UpdateMeasurement(Measurement measurement , string SSN)
        {
            try
            {
                //map the measurement to the measurement entity
                var measurementEntity = _mapper.Map<MeasurmentBe>(measurement);
                measurementEntity.PatientSSN = SSN;
                //update the measurement in the database
                _Repo.UpdateMeasurement(measurementEntity);
            }
            catch (Exception e)
            {
                throw new Exception("Error in updating measurement", e);
            }
        }

        private bool ValidateSSN(string SSN)
        {
            //get the patient from the patient service, that takes SSN as a parameter
            var result = _HttpClient.GetAsync($"/patient/get?ssn={SSN}").Result;
            Log.Logger.Debug($"Validating SSN {SSN}");
            Log.Logger.Debug($"Response from patient service {result}");
            var content = result.Content.ReadAsStringAsync().Result;
            //check if the patient exists
            if (!content.IsNullOrEmpty())
            {
                return true;
            }
            else { return false; }
        }   
    }
}
