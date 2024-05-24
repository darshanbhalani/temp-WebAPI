using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using temp_WebAPI.Models;

namespace temp_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AQIController : Controller
    {
        private readonly NpgsqlConnection _connection;
        public AQIController(NpgsqlConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }

        [HttpGet("GetAllIncidents")]
        public async Task<IActionResult> GetAllIncidents()
        {
            List<AQI> incidents = new List<AQI>();
            using (var command = new NpgsqlCommand("select * from getaqiincidents()", _connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    incidents.Add(new AQI
                    {
                        Id = reader.GetInt64(0),
                        PollNo = reader.GetInt64(1),
                        Area = reader.GetString(2),
                        Description = reader.GetString(3),
                        Coordinates = JsonConvert.DeserializeObject<Coordinates>(reader.GetString(4)),
                        TimeStamp = reader.GetDateTime(5),
                    });
                }
                return Ok(new {success=true, body = incidents});
            }
        } 
    }
}
