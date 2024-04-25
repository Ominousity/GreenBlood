using AutoMapper;
using Domain;
using Newtonsoft.Json;
using Patient_Repo;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService
{
    public class PatientService : IPatientService
    {
        IMapper _mapper;
        IPatientRepository _patientRepository;
        HttpClient _HttpClient;
        public PatientService(IMapper mapper, IPatientRepository patientRepository)
        {
            _mapper = mapper;
            _patientRepository = patientRepository;
            _HttpClient = new HttpClient { BaseAddress = new Uri("http://measurement-service/Measurment") };
        }
        public void AddPatient(Patient patient)
        {
            // Map the Patient object to PatientBe object
            PatientBe patientBe = _mapper.Map<PatientBe>(patient);
            _patientRepository.AddPatient(patientBe);

            if (patient.Measurements.Count != 0)
            {
                var response = _HttpClient.PostAsync($"/AddMeasurements", new StringContent(JsonConvert.SerializeObject(patient.Measurements), Encoding.UTF8, "application/json"));
            }
        }

        public void DeletePatient(int ssn)
        {
            _patientRepository.DeletePatient(ssn);
        }

        public async Task<Patient> GetPatient(int ssn)
        {
            //get patientBe and map to patient
            PatientBe patientBe = await _patientRepository.GetPatient(ssn);
            Patient patient = _mapper.Map<Patient>(patientBe);

            //TODO: retrive measurments for this patient from http client
            var response = await _HttpClient.GetAsync($"/GetMeasurements/{ssn}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var measurements = JsonConvert.DeserializeObject<List<Measurement>>(content);
                patient.Measurements = measurements;
            }

            return patient;
        }
    }
}
