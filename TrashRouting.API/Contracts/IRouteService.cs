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

        [Get("route/list")]
        Task<IEnumerable<Route>> Routes();

        [Get("route/{id}")]
        Task<Route> RouteById([Path] int id);
    }
}
