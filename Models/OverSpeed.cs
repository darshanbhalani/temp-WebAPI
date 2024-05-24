using System.Text.Json.Nodes;

namespace temp_WebAPI.Models
{
    public class OverSpeed
    {
        public long Id {  get; set; }
        public string VehicleNo { get; set; }
        public string Description { get; set; }
        public Coordinates Coordinates { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
