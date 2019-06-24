using Microsoft.AspNetCore.Mvc;
using RestEase;
using System.Net.Http;
using System.Threading.Tasks;
using TrashRouting.API.Commands;
using TrashRouting.API.Models;

namespace TrashRouting.API.Contracts
{
    public interface IClusterService
    {
        [AllowAnyStatusCode]
        [Get("cluster/algdata")]
        Task<ClusterAlgData> AlgData();

        [AllowAnyStatusCode]
        [Get("cluster/point/{id}")]
        Task<Point> Point([Path] int id);

        [AllowAnyStatusCode]
        [Get("cluster/riskypoint/{id}")]
        Task<Point> RiskyPoint([Path] int id);

        [AllowAnyStatusCode]
        [Post("cluster/schedule")]
        Task<Response<HttpResponseMessage>> Schedule([Body] ScheduleClusterAlgorithmCommand command);
    }
}
