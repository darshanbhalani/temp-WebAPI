using Microsoft.AspNetCore.Mvc;
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
                        AverageSpeed=reader.GetDouble(2),
                        ThresholdSpeed = reader.GetInt32(3),
                        Interval = reader.GetInt32(4),
                        StartTime = reader.GetDateTime(5),
                        EndTime = reader.GetDateTime(6),
                        Authority=reader.GetString(7),
                        City=reader.GetString(8),
                        State=reader.GetString(9),
                        CreatedOn = reader.GetDateTime(10),
                    });
                }
                return Ok(new { success = true, lastExecutionTime = incidents.Max(d => d.CreatedOn), body = incidents });
            }
        }

        [HttpGet("SyncNewIncidents")]
        public async Task<IActionResult> SyncNewIncidents(string lastExecutionTime)
        {
            try
            {
                List<OverSpeed> incidents = new List<OverSpeed>();
                using (var command = new NpgsqlCommand($"select * from syncoverspeedincidents('{lastExecutionTime}')", _connection))
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        while (reader.Read())
                        {
                            incidents.Add(new OverSpeed
                            {
                                Id = reader.GetInt64(0),
                                VehicleNumber = reader.GetString(1),
                                AverageSpeed = reader.GetDouble(2),
                                ThresholdSpeed = reader.GetInt32(3),
                                Interval = reader.GetInt32(4),
                                StartTime = reader.GetDateTime(5),
                                EndTime = reader.GetDateTime(6),
                                Authority = reader.GetString(7),
                                City = reader.GetString(8),
                                State = reader.GetString(9),
                                CreatedOn = reader.GetDateTime(10),
                            });
                        }
                        return Ok(new { success = true, lastExecutionTime = incidents.Max(d => d.CreatedOn), body = incidents });
                    }
                    else
                    {
                        return Ok(new { success = true, lastExecutionTime = lastExecutionTime, body = incidents });

                    }
                }
            }
            catch (NpgsqlException pex)
            {
                return Ok(new { success = true, error = pex.Message });
            }
            catch (Exception ex)
            {
                return Ok(new { success = true,error= ex.Message});
            }
        }

        [HttpGet("GetAllConfigurations")]
        public async Task<IActionResult> GetAllConfigurations()
        {
            List<Configuration> configurations = new List<Configuration>();
            using (var command = new NpgsqlCommand("select * from getoverspeedconfigurations()", _connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    configurations.Add(new Configuration
                    {
                        ConfigurationId = reader.GetInt32(0),
                        ConfigurationName = reader.GetString(1),
                        ConfigurationThreshold = reader.GetInt32(2),
                        ConfigurationInterval = reader.GetInt32(3),
                    });
                }
                return Ok(new { success = true, body = configurations });
            }
        }

        [HttpPost("UpdateConfiguration")]
        public async Task<IActionResult> UpdateConfiguration(Configuration newConfiguration)
        {
            using (var command = new NpgsqlCommand($"select updateoverspeedconfiguration({newConfiguration.ConfigurationThreshold},{newConfiguration.ConfigurationInterval})", _connection))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (reader.GetBoolean(0))
                    {
                        return Ok(new { success = true, body = new { message = "Configuration details updated successfully." } });
                    }
                    else
                    {
                        return Ok(new { success = false, body = new { message = "Unable to update Configuration details." } });
                    }
                }
                return Ok(new { success = false, body = new { message = "Somthing went wrong." } });
            }
        }
    }
}

