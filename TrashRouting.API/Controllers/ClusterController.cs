using Consul;
using Microsoft.AspNetCore.Mvc;
using RestEase;
using System.Linq;
using System.Threading.Tasks;
using TrashRouting.API.Contracts;
using TrashRouting.API.Models;

namespace TrashRouting.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClusterController : ControllerBase
    {
        private readonly IClusterService clusterService;

        public ClusterController(IConsulClient consulClient)
        {
            var query = consulClient.Catalog.Service("service-cluster").GetAwaiter().GetResult();
            var serviceInstance = query.Response.First();
            clusterService = RestClient
                .For<IClusterService>($"{serviceInstance.ServiceAddress}:{serviceInstance.ServicePort}");
        }

        [HttpGet("algdata")]
        public async Task<ClusterAlgData> AlgData() 
            => await clusterService.AlgData();

        [HttpGet("point/{id}")]
        public async Task<Point> Point(int id)
            => await clusterService.Point(id);

        [HttpGet("riskypoint/{id}")]
        public async Task<Point> RiskyPoint(int id)
            => await clusterService.RiskyPoint(id);
    }
}