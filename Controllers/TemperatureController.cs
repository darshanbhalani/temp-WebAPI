//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Npgsql;
//using temp_WebAPI.Models;

//namespace temp_WebAPI.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class TemperatureController : ControllerBase
//    {
//        private readonly NpgsqlConnection _connection;
//        public TemperatureController(NpgsqlConnection connection)
//        {
//            _connection = connection;
//            _connection.Open();
//        }

//        [HttpGet("GetAllIncidents")]
//        public async Task<IActionResult> GetAllIncidents()
//        {
//            List<Temperature> incidents = new List<Temperature>();
//            using (var command = new NpgsqlCommand("select * from getalltemperatureincidents()", _connection))
//            using (var reader = command.ExecuteReader())
//            {
//                while (reader.Read())
//                {
//                    incidents.Add(new Temperature
//                    {
//                        PollNumber=reader.GetInt64(0),
//                        Area = reader.GetString(1),
//                        Description = reader.GetString(2),
//                        Threshold=reader.GetInt32(3),
//                         = reader.GetInt32(4),
//                        StartTime = reader.GetDateTime(5),
//                        EndTime = reader.GetDateTime(6),
//                    });
//                }
//                return Ok(new { success = true, body = incidents });
//            }
//        }
//    }
//}
