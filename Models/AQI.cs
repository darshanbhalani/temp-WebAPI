using System.Text.Json.Nodes;

namespace temp_WebAPI.Models
{
    public class AQI
    {
        public long Id { get; set; }
        public long PollNo { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public Coordinates Coordinates { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
