using System.Text.Json.Nodes;

namespace temp_WebAPI.Models
{
    public class OverSpeed
    {
        public long Id {  get; set; }
        public string VehicleNumber { get; set; }
        public int ThresholdSpeed { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
