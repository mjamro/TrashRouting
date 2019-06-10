using Consul;
using Microsoft.AspNetCore.Mvc;
using RestEase;
using System.Linq;
using System.Threading.Tasks;
using TrashRouting.API.Contracts;
using TrashRouting.API.Models;

namespace TrashRouting.API.Controllers
{
    [Route("api/[controller]")]
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

        public async Task<ClusterAlgData> GetClusterAlgData() 
            => await clusterService.GetClusterAlgData();

    }
}