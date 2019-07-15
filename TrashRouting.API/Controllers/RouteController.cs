using Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestEase;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TrashRouting.API.Commands;
using TrashRouting.API.Contracts;
using TrashRouting.API.Models;

namespace TrashRouting.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService routeService;

        // First phase - HARDCODING! :O ;_;
        //public RouteController()
        //{
        //    routeService = RestClient.For<IRouteService>("http://localhost:5020");
        //}


        // Second phase - service address from appsettings 
        //public RouteController(IConsulClient consulClient, IConfiguration configuration)
        //{
        //    routeService = RestClient.For<IRouteService>(configuration["Services:Route:Address"]);
        //}


        // Third phase - address from consul registry
        public RouteController(IConsulClient consulClient)
        {
            var query = consulClient.Catalog.Service("service-routes").GetAwaiter().GetResult();
            var serviceInstance = query.Response.First();
            routeService = RestClient
                .For<IRouteService>($"{serviceInstance.ServiceAddress}:{serviceInstance.ServicePort}");
        }

        [HttpGet("list")]
        public async Task<IEnumerable<Route>> List()
        {
            routeService.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                Request.Headers["Authorization"].ToString().Substring(7));

            return await routeService.Routes();
        }

        [HttpGet("{id}")]
        public async Task<Route> RouteById(int id)
        {
            routeService.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                Request.Headers["Authorization"].ToString().Substring(7));

            return await routeService.RouteById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddRouteCommand command)
        {
            routeService.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                Request.Headers["Authorization"].ToString().Substring(7));

            var response = await routeService.Post(command);
            return StatusCode((int)response.ResponseMessage.StatusCode);
        }
    }
}
