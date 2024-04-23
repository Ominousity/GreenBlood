using Measurments_Service.Be;

namespace Measurments_Service.Repository
{
    public interface IMeasurmentRepo
    {
        List<MeasurmentBe> GetMeasurements(string SSn);

        void AddMeasurement(MeasurmentBe measurement);

        void UpdateMeasurement(MeasurmentBe measurement);
    }
}
