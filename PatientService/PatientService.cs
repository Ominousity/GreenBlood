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
            _HttpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8082") };
        }
        public void AddPatient(Patient patient)
        {
            // Map the Patient object to PatientBe object
            PatientBe patientBe = _mapper.Map<PatientBe>(patient);
            _patientRepository.AddPatient(patientBe);

            if (patient.Measurements.Count != 0)
            {
                _HttpClient.PostAsync($"/Measurment/Add", new StringContent(JsonConvert.SerializeObject(patient.Measurements), Encoding.UTF8, "application/json"));
            }
        }

        public void DeletePatient(string ssn)
        {
            _patientRepository.DeletePatient(ssn);
        }

        public async Task<Patient> GetPatient(string ssn)
        {
            //get patientBe and map to patient
            PatientBe patientBe = await _patientRepository.GetPatient(ssn);
            Patient patient = _mapper.Map<Patient>(patientBe);

            //TODO: retrive measurments for this patient from http client
            var response = await _HttpClient.GetAsync($"/Get?SSn={ssn}");
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
