using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestEase;
using System.Threading.Tasks;
using TrashRouting.API.Commands;
using TrashRouting.API.Contracts;
using TrashRouting.Common.Messaging;
using TrashRouting.Common.RabbitMQ;

namespace TrashRouting.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly IBusPublisher busPublisher;
        private readonly ISyncService syncService;

        //public SyncController(IBusPublisher busPublisher)
        //{
        //    this.busPublisher = busPublisher;
        //}

        //public SyncController(IConsulClient consulClient)
        //{
        //    var query = consulClient.Catalog.Service("service-sync").GetAwaiter().GetResult();
        //    var serviceInstance = query.Response.First();
        //    clusterService = RestClient
        //        .For<ISyncService>($"{serviceInstance.ServiceAddress}:{serviceInstance.ServicePort}");
        //}

        public SyncController(IBusPublisher busPublisher, IConfiguration configuration)
        {
            this.busPublisher = busPublisher;
            syncService = RestClient.For<ISyncService>($"{configuration["Fabio:Url"]}/sync");
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> ScheduleSynchronization(ScheduleSynchronizationCommand command)
        {
            await busPublisher.SendAsync(command, CorrelationContext.Create());

            return Accepted();
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptSynchronization(AcceptSynchronizationCommand command)
        {
            await busPublisher.SendAsync(command, CorrelationContext.Create());

            return Accepted();
        }
    }
}