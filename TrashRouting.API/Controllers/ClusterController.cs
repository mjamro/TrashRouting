using Consul;
using Microsoft.AspNetCore.Mvc;
using Polly;
using RestEase;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TrashRouting.API.Commands;
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

        [HttpPost("schedule")]
        public async Task<IActionResult> Schedule(ScheduleClusterAlgorithmCommand command)
        {
            return await Policy.Handle<ApiException>(ex => ex.StatusCode == HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    var response = await clusterService.Schedule(command);
                    return StatusCode((int)response.ResponseMessage.StatusCode);
                });
        }

    }
}