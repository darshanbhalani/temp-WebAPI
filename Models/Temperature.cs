namespace temp_WebAPI.Models
{
    public class Temperature
    {
        public double AverageTemperature { get; set; }
        public long PollNumber { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public int Threshold { get; set; }
        public int Interval { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
