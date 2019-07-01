using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrashRouting.API.Commands;
using TrashRouting.Common.RabbitMq;
using TrashRouting.Common.RabbitMQ;

namespace TrashRouting.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly IBusPublisher busPublisher;

        public SyncController(IBusPublisher busPublisher)
        {
            this.busPublisher = busPublisher;
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> ScheduleSynchronization(ScheduleSynchronizationCommand command)
        {
            //TODO: CorrelationContext
            await busPublisher.SendAsync(command, CorrelationContext.Create());

            return Accepted();
        }

    }
}