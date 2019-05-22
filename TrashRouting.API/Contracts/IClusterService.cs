using RestEase;
using System.Threading.Tasks;
using TrashRouting.API.Models;

namespace TrashRouting.API.Contracts
{
    public interface IClusterService 
    {
        [Get("api/cluster")]
        Task<ClusterAlgData> GetClusterAlgData();
    }
}
