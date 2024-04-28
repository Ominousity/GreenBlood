using Measurments_Service.Be;

namespace Measurments_Service.Repository
{
    public class MeasurmentRepo : IMeasurmentRepo
    {

        public readonly MeasurmentContext _dbContext;
        public MeasurmentRepo(MeasurmentContext measurmentContext) 
        {
            _dbContext = measurmentContext;
        }
        public void AddMeasurement(MeasurmentBe measurement)
        {
            try
            {
                measurement.Date = DateTime.UtcNow;
                _dbContext.Measurments.Add(measurement);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<MeasurmentBe> GetMeasurements(string SSn)
        {
            try
            {                 
                return _dbContext.Measurments.Where(m => m.PatientSSN == SSn).ToList();}
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public void UpdateMeasurement(MeasurmentBe measurement)
        {
            try
            {
                _dbContext.Measurments.Update(measurement);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
