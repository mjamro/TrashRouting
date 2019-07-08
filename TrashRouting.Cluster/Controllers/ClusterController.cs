using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrashRouting.Cluster.Commands;
using TrashRouting.Cluster.Models;

namespace TrashRouting.Cluster.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClusterController : ControllerBase
    {
        [HttpGet("algdata")]
        public async Task<ClusterAlgData> AlgData()
            => GetClusterAlgData();

        [HttpGet("point/{id}")]
        public async Task<Point> Point(int id)
        {
            return new Point
            {
                Id = id,
                Type = "attraction",
                Latitude = 52.53223,
                Longitude = 23.24135
            };
        }

        [HttpGet("riskypoint/{id}")]
        public async Task<Point> RiskyPoint(int id)
        {
            if (id > 5)
                throw new Exception("Getting point from database failed.");

            return new Point
            {
                Id = id,
                Type = "attraction",
                Latitude = 52.53223,
                Longitude = 23.24135
            };
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> Schedule(ScheduleClusterAlgorithmCommand command)
        {
            if (command.PointsNumber > 1000)
                return StatusCode(500);

            if (command.PointsNumber < 0)
                return BadRequest();

            return Ok();
        }

        private static ClusterAlgData GetClusterAlgData()
        {
            return new ClusterAlgData()
            {
                ClusterId = 1,
                CompanyName = "Trash Company",
                TrucksCount = 150,
                Points = new List<Point>()
                {
                    new Point()
                    {
                        Id = 1,
                        Type = "Normal",
                        Latitude = 22.131513,
                        Longitude = 53.235255,
                    },
                    new Point()
                    {
                        Id = 2,
                        Type = "Normal",
                        Latitude = 22.65656,
                        Longitude = 53.001243,

                    },
                    new Point()
                    {
                        Id = 3,
                        Type = "Normal",
                        Latitude = 23.542534,
                        Longitude = 53.123352,

                    },
                    new Point()
                    {
                        Id = 4,
                        Type = "Normal",
                        Latitude = 22.543533,
                        Longitude = 53.434335,

                    }
                }
            };
        }
    }
}
