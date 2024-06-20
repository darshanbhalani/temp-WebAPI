using System.Text.Json.Nodes;

namespace temp_WebAPI.Models
{
    public class OverSpeed
    {
        public long Id {  get; set; }
        public string VehicleNumber { get; set; }
        public double AverageSpeed { get; set; }
        public int ThresholdSpeed { get; set; }
        public int Interval { get; set; }
        public string Authority { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
