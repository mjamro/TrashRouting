using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestEase;
using TrashRouting.API.Contracts;
using TrashRouting.API.Models;

namespace TrashRouting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterController : ControllerBase
    {
        private readonly IClusterService clusterService;

        public ClusterController()
        {
            clusterService = RestClient.For<IClusterService>("http://localhost:5001");
        }

        public async Task<ClusterAlgData> GetClusterAlgData() 
            => await clusterService.GetClusterAlgData();

    }
}