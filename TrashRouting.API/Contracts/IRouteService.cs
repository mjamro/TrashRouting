using RestEase;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TrashRouting.API.Models;

namespace TrashRouting.API.Contracts
{
    public interface IRouteService
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        [Get("api/route")]
        Task<IEnumerable<Route>> GetRoutes();

        [Get("api/route/{id}")]
        Task<Route> GetRouteById([Path] int id);
    }
}
