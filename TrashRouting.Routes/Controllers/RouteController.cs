using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TrashRouting.Routes.Models;

namespace TrashRouting.Routes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<Route> GetRouteById(int id)
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IEnumerable<Route>> GetRoutes()
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
    }
}
