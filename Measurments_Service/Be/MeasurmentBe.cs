﻿namespace Measurments_Service.Be
{
    public class MeasurmentBe
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public string PatientSSN { get; set; }
        public bool Seen { get; set; }
    }
}
