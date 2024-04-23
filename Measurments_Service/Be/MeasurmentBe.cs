namespace Measurments_Service.Be
{
    public class MeasurmentDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }

        public string SSn { get; set; }
        public bool Seen { get; set; }
    }
}
