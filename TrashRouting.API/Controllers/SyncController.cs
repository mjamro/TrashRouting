using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrashRouting.API.Commands;
using TrashRouting.Common.RabbitMq;
using TrashRouting.Common.RabbitMQ;

namespace TrashRouting.API.Controllers
{
    [Route("api/[controller]")]
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
            //var context = CorrelationContext.Create();
            await busPublisher.SendAsync(command, new CorrelationContext());

            return Accepted();
        }

    }
}