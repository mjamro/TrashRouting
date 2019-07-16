using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrashRouting.Common.RabbitMQ;
using TrashRouting.Routes.Commands;
using TrashRouting.Routes.Models;

namespace TrashRouting.Routes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class RouteController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<Route> Route(int id)
        {
            return new Route()
            {
                Id = id,
                Start = new Point()
                {
                    Id = 3,
                    Type = "Normal",
                    Latitude = 22.432524,
                    Longitude = 52.432524
                },
                Target = new Point()
                {
                    Id = 52,
                    Type = "Normal",
                    Latitude = 21.43252,
                    Longitude = 52.65324
                }
            };
        }

        [HttpGet("list")]
        public async Task<IEnumerable<Route>> List()
        {
            return new List<Route>()
            {
                new Route()
                {
                    Id = 1,
                    Start = new Point()
                    {
                        Id = 3,
                        Type = "Normal",
                        Latitude = 22.432524,
                        Longitude = 52.432524
                    },
                    Target = new Point()
                    {
                        Id = 52,
                        Type = "Normal",
                        Latitude = 21.43252,
                        Longitude = 52.65324
                    }
                },
            new Route()
            {
                Id = 2,
                Start = new Point()
                {
                    Id = 3,
                    Type = "Normal",
                    Latitude = 22.432524,
                    Longitude = 52.432524
                },
                Target = new Point()
                {
                    Id = 52,
                    Type = "Normal",
                    Latitude = 21.43252,
                    Longitude = 52.65324
                }
            }
            };
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddRouteCommand command)
        {
            if (command.Id <= 0 )
                return BadRequest();

            return Ok();
        }

    }
}
