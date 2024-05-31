using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using temp_WebAPI.Models;

namespace temp_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OverSpeedController : Controller
    {
        private readonly NpgsqlConnection _connection;
        public OverSpeedController(NpgsqlConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }

        [HttpGet("GetAllIncidents")]
        public async Task<IActionResult> GetAllIncidents()
        {
            List<OverSpeed> incidents = new List<OverSpeed>();
            using (var command = new NpgsqlCommand("select * from getoverspeedincidents()", _connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    incidents.Add(new OverSpeed
                    {
                        Id = reader.GetInt64(0),
                        VehicleNumber = reader.GetString(1),
                        ThresholdSpeed = reader.GetInt32(2),
                        Description = reader.GetString(3),
                        StartTime = reader.GetDateTime(4),
                        EndTime = reader.GetDateTime(5),
                    });
                }
                return Ok(new { success = true, body = incidents });
            }
        }

    }
}
