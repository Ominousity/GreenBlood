using Domain;

namespace Measurments_Service.Service
{
    public interface IMeasurmentService
    {
        List<Measurement> GetMeasurements(string SSn);

        void AddMeasurement(Measurement measurement);

        void UpdateMeasurement(Measurement measurement);
    }
}
