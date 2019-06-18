using Microsoft.AspNetCore.Mvc;
using RestEase;
using System.Threading.Tasks;
using TrashRouting.API.Commands;
using TrashRouting.API.Models;

namespace TrashRouting.API.Contracts
{
    public interface IClusterService 
    {
        [Get("cluster/algdata")]
        Task<ClusterAlgData> AlgData();

        [Get("cluster/point/{id}")]
        Task<Point> Point([Path] int id);

        [Get("cluster/riskypoint/{id}")]
        Task<Point> RiskyPoint([Path] int id);

        [Post("cluster/schedule")]
        Task<IActionResult> Schedule([Body] ScheduleClusterAlgorithmCommand command);
    }
}
