using RestEase;
using System.Threading.Tasks;
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
    }
}
