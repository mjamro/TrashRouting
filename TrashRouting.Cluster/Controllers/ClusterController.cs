using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrashRouting.Cluster.Models;

namespace TrashRouting.Cluster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterController : ControllerBase
    {
        public async Task<ClusterAlgData> GetClusterAlgData()
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
