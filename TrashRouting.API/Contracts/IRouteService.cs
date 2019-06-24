using Microsoft.AspNetCore.Mvc;
using RestEase;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TrashRouting.API.Commands;
using TrashRouting.API.Models;

namespace TrashRouting.API.Contracts
{
    public interface IRouteService
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        [AllowAnyStatusCode]
        [Get("route/list")]
        Task<IEnumerable<Route>> Routes();

        [AllowAnyStatusCode]
        [Get("route/{id}")]
        Task<Route> RouteById([Path] int id);

        [AllowAnyStatusCode]
        [Post("route")]
        Task<Response<HttpResponseMessage>> Post([Body] AddRouteCommand command);
    }
}
