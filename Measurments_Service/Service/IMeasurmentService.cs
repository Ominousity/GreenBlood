using Domain;

namespace Measurments_Service.Service
{
    public interface IMeasurmentService
    {
        List<Measurement> GetMeasurements(string SSn);

        void AddMeasurement(Measurement measurement, string SSN);

        void UpdateMeasurement(Measurement measurement, string SSN);
    }
}
