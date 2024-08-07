﻿using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
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
            try
            {
                List<AQI> incidents = new List<AQI>();

                using (var cmd = new NpgsqlCommand("SELECT * FROM getalloveraqi2()", _connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            incidents.Add(new AQI
                            {
                                PollNumber = reader.GetInt64(reader.GetOrdinal("pollnumber")),
                                Avg_AQI = reader.GetInt32(reader.GetOrdinal("avg_aqi")),
                                Category = reader.GetString(reader.GetOrdinal("category")),
                                Area = reader.GetString(reader.GetOrdinal("area")),
                                City = reader.GetString(reader.GetOrdinal("city")),
                                State = reader.GetString(reader.GetOrdinal("state")),
                                Min_PM10 = reader.GetDouble(reader.GetOrdinal("min_pm10")),
                                Max_PM10 = reader.GetDouble(reader.GetOrdinal("max_pm10")),
                                Avg_PM10 = reader.GetDouble(reader.GetOrdinal("avg_pm10")),
                                Min_PM2_5 = reader.GetDouble(reader.GetOrdinal("min_pm2_5")),
                                Max_PM2_5 = reader.GetDouble(reader.GetOrdinal("max_pm2_5")),
                                Avg_PM2_5 = reader.GetDouble(reader.GetOrdinal("avg_pm2_5")),
                                Min_NO2 = reader.GetDouble(reader.GetOrdinal("min_no2")),
                                Max_NO2 = reader.GetDouble(reader.GetOrdinal("max_no2")),
                                Avg_NO2 = reader.GetDouble(reader.GetOrdinal("avg_no2")),
                                Min_O3 = reader.GetDouble(reader.GetOrdinal("min_o3")),
                                Max_O3 = reader.GetDouble(reader.GetOrdinal("max_o3")),
                                Avg_O3 = reader.GetDouble(reader.GetOrdinal("avg_o3")),
                                Min_CO = reader.GetDouble(reader.GetOrdinal("min_co")),
                                Max_CO = reader.GetDouble(reader.GetOrdinal("max_co")),
                                Avg_CO = reader.GetDouble(reader.GetOrdinal("avg_co")),
                                Min_SO2 = reader.GetDouble(reader.GetOrdinal("min_so2")),
                                Max_SO2 = reader.GetDouble(reader.GetOrdinal("max_so2")),
                                Avg_SO2 = reader.GetDouble(reader.GetOrdinal("avg_so2")),
                                Min_NH3 = reader.GetDouble(reader.GetOrdinal("min_nh3")),
                                Max_NH3 = reader.GetDouble(reader.GetOrdinal("max_nh3")),
                                Avg_NH3 = reader.GetDouble(reader.GetOrdinal("avg_nh3")),
                                Min_PB = reader.GetDouble(reader.GetOrdinal("min_pb")),
                                Max_PB = reader.GetDouble(reader.GetOrdinal("max_pb")),
                                Avg_PB = reader.GetDouble(reader.GetOrdinal("avg_pb")),
                                Min_Temperature = reader.GetDouble(reader.GetOrdinal("min_temperature")),
                                Max_Temperature = reader.GetDouble(reader.GetOrdinal("max_temperature")),
                                Avg_Temperature = reader.GetDouble(reader.GetOrdinal("avg_temperature")),
                                Min_Wind = reader.GetDouble(reader.GetOrdinal("min_wind")),
                                Max_Wind = reader.GetDouble(reader.GetOrdinal("max_wind")),
                                Avg_Wind = reader.GetDouble(reader.GetOrdinal("avg_wind")),
                                Min_Pressure = reader.GetDouble(reader.GetOrdinal("min_pressure")),
                                Max_Pressure = reader.GetDouble(reader.GetOrdinal("max_pressure")),
                                Avg_Pressure = reader.GetDouble(reader.GetOrdinal("avg_pressure")),
                                Min_Precip = reader.GetDouble(reader.GetOrdinal("min_precip")),
                                Max_Precip = reader.GetDouble(reader.GetOrdinal("max_precip")),
                                Avg_Precip = reader.GetDouble(reader.GetOrdinal("avg_precip")),
                                Min_Visibility = reader.GetDouble(reader.GetOrdinal("min_visibility")),
                                Max_Visibility = reader.GetDouble(reader.GetOrdinal("max_visibility")),
                                Avg_Visibility = reader.GetDouble(reader.GetOrdinal("avg_visibility")),
                                Min_Humidity = reader.GetDouble(reader.GetOrdinal("min_humidity")),
                                Max_Humidity = reader.GetDouble(reader.GetOrdinal("max_humidity")),
                                Avg_Humidity = reader.GetDouble(reader.GetOrdinal("avg_humidity")),
                                Min_Uv = reader.GetDouble(reader.GetOrdinal("min_uv")),
                                Max_Uv = reader.GetDouble(reader.GetOrdinal("max_uv")),
                                Avg_Uv = reader.GetDouble(reader.GetOrdinal("avg_uv")),
                                Min_Gust = reader.GetDouble(reader.GetOrdinal("min_gust")),
                                Max_Gust = reader.GetDouble(reader.GetOrdinal("max_gust")),
                                Avg_Gust = reader.GetDouble(reader.GetOrdinal("avg_gust")),
                                Min_Feelslike = reader.GetDouble(reader.GetOrdinal("min_feelslike")),
                                Max_Feelslike = reader.GetDouble(reader.GetOrdinal("max_feelslike")),
                                Avg_Feelslike = reader.GetDouble(reader.GetOrdinal("avg_feelslike")),
                                StartTime = reader.GetDateTime(reader.GetOrdinal("starttime")),
                                EndTime = reader.GetDateTime(reader.GetOrdinal("endtime"))
                            });
                        }
                    }
                }

                return Json(new { success = true, body = incidents });
            }
            catch (NpgsqlException nex)
            {
                return Ok(new { success = false, error = nex.Message });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, error = ex.Message });
            }
        }

        [HttpGet("GetAllConfigurations")]
        public async Task<IActionResult> GetAllConfigurations()
        {
            try
            {
                AQIConfiguration configurations = new AQIConfiguration();
                using (NpgsqlCommand cmd = new NpgsqlCommand($"select * from getaqiconfigurations()", _connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            configurations.Categories.Add(reader.GetString(1));
                            configurations.PM10Breakpoints.Add(reader.GetDouble(2));
                            configurations.PM10Breakpoints.Add(reader.GetDouble(3));
                            configurations.PM25Breakpoints.Add(reader.GetDouble(4));
                            configurations.PM25Breakpoints.Add(reader.GetDouble(5));
                            configurations.NO2Breakpoints.Add(reader.GetDouble(6));
                            configurations.NO2Breakpoints.Add(reader.GetDouble(7));
                            configurations.O3Breakpoints.Add(reader.GetDouble(8));
                            configurations.O3Breakpoints.Add(reader.GetDouble(9));
                            configurations.COBreakpoints.Add(reader.GetDouble(10));
                            configurations.COBreakpoints.Add(reader.GetDouble(11));
                            configurations.SO2Breakpoints.Add(reader.GetDouble(12));
                            configurations.SO2Breakpoints.Add(reader.GetDouble(13));
                            configurations.NH3Breakpoints.Add(reader.GetDouble(14));
                            configurations.NH3Breakpoints.Add(reader.GetDouble(15));
                            configurations.PBBreakpoints.Add(reader.GetDouble(16));
                            configurations.PBBreakpoints.Add(reader.GetDouble(17));
                        }
                    }
                }

                using (NpgsqlCommand cmd = new NpgsqlCommand($"select * from getaqicategoryrange()", _connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            configurations.CategoriesRange.Add(reader.GetDouble(1));
                        }
                    }
                }

                return Json(new { success = true, body = configurations });
            }
            catch (NpgsqlException nex)
            {
                return Ok(new { success = false, error = nex.Message });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, error = ex.Message });
            }
        }

        [HttpPost("UpdateConfiguration")]
        public async Task<IActionResult> UpdateConfiguration(AQIConfiguration configuration)
        {
            try
            {
                using (var cmd = new NpgsqlCommand("SELECT updateaqiParameters(@in_pm10_breakpoints, @in_pm25_breakpoints, @in_no2_breakpoints, @in_o3_breakpoints, @in_co_breakpoints, @in_so2_breakpoints, @in_nh3_breakpoints, @in_pb_breakpoints)", _connection))
                {
                    cmd.Parameters.AddWithValue("in_pm10_breakpoints", NpgsqlTypes.NpgsqlDbType.Double | NpgsqlTypes.NpgsqlDbType.Array, configuration.PM10Breakpoints.ToArray());
                    cmd.Parameters.AddWithValue("in_pm25_breakpoints", NpgsqlTypes.NpgsqlDbType.Double | NpgsqlTypes.NpgsqlDbType.Array, configuration.PM25Breakpoints.ToArray());
                    cmd.Parameters.AddWithValue("in_no2_breakpoints", NpgsqlTypes.NpgsqlDbType.Double | NpgsqlTypes.NpgsqlDbType.Array, configuration.NO2Breakpoints.ToArray());
                    cmd.Parameters.AddWithValue("in_o3_breakpoints", NpgsqlTypes.NpgsqlDbType.Double | NpgsqlTypes.NpgsqlDbType.Array, configuration.O3Breakpoints.ToArray());
                    cmd.Parameters.AddWithValue("in_co_breakpoints", NpgsqlTypes.NpgsqlDbType.Double | NpgsqlTypes.NpgsqlDbType.Array, configuration.COBreakpoints.ToArray());
                    cmd.Parameters.AddWithValue("in_so2_breakpoints", NpgsqlTypes.NpgsqlDbType.Double | NpgsqlTypes.NpgsqlDbType.Array, configuration.SO2Breakpoints.ToArray());
                    cmd.Parameters.AddWithValue("in_nh3_breakpoints", NpgsqlTypes.NpgsqlDbType.Double | NpgsqlTypes.NpgsqlDbType.Array, configuration.NH3Breakpoints.ToArray());
                    cmd.Parameters.AddWithValue("in_pb_breakpoints", NpgsqlTypes.NpgsqlDbType.Double | NpgsqlTypes.NpgsqlDbType.Array, configuration.PBBreakpoints.ToArray());

                    var result = await cmd.ExecuteScalarAsync();
                }
                return Json(new { success = true, message = new { message = "Configuration details updated successfully." } });
            }
            catch (NpgsqlException nex)
            {
                return Ok(new
                {
                    success = false,
                    error = nex.Message
                });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, error = ex.Message });
            }
        }

        [HttpPost("UpdateOneConfiguration")]

        public async Task<IActionResult> UpdateOneConfiguration(string compound, double[] breakpoints)
        {
            try
            {
                string sql = $"SELECT updateaqiParameter1('{compound}', @in_breakpoints)";

                using (var cmd = new NpgsqlCommand(sql, _connection))
                {
                    cmd.Parameters.AddWithValue("in_breakpoints", NpgsqlTypes.NpgsqlDbType.Double | NpgsqlTypes.NpgsqlDbType.Array, breakpoints);

                    var result = await cmd.ExecuteScalarAsync();
                    Console.WriteLine("The result is : " + result);
                    if ((bool)result)
                    {
                        return Json(new { success = true, message = "Configuration details updated successfully." });
                    }
                    else
                    {
                        return Json(new { success = false, error = "Failed to update configuration." });
                    }
                }
            }
            catch (NpgsqlException nex)
            {
                return Ok(new
                {
                    success = false,
                    error = nex.Message
                });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, error = ex.Message });
            }
        }
    }
}
