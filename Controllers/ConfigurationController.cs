using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using temp_WebAPI.Models;

namespace temp_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly NpgsqlConnection _connection;
        public ConfigurationController(NpgsqlConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }

        [HttpGet("GetAllConfigurations")]
        public async Task<IActionResult> GetAllConfigurations()
        {
            List<Configuration> configurations = new List<Configuration>();
            using (var command = new NpgsqlCommand("select * from getallconfigurations()", _connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   configurations.Add( new Configuration { 
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
            using (var command = new NpgsqlCommand($"select updateconfiguration({newConfiguration.ConfigurationId},{newConfiguration.ConfigurationThreshold},{newConfiguration.ConfigurationInterval})", _connection))
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
