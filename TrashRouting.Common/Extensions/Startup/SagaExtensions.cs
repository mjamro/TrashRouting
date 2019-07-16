using Microsoft.Extensions.DependencyInjection;
using TrashRouting.Common.Saga;

namespace TrashRouting.Common.Extensions.Startup
{
    public static class SagaExtensions
    {
        public static void AddSaga(this IServiceCollection services)
        {
            // Main configuration
            services.AddTransient<ISagaCoordinator, SagaCoordinator>();
        }
    }
}
