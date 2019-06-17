using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace TrashRouting.Common.Extensions.Startup
{
    public static class ConsulExtensions
    {
        public static IServiceCollection AddConsul(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri(configuration["Consul:Address"]);
            }));

            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app,
         IApplicationLifetime lifetime)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var configuration = scope.ServiceProvider.GetService<IConfiguration>();

                var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();

                var uri = new Uri(configuration["Consul:ServiceAddress"]);
                var registration = new AgentServiceRegistration()
                {
                    ID = $"{configuration["Consul:ServiceID"]}-{uri.Port}",
                    Name = configuration["Consul:ServiceName"],
                    Address = $"{uri.Scheme}://{uri.Host}",
                    Port = uri.Port,
                    Tags = configuration.GetSection("Consul:Tags")
                    .AsEnumerable()
                    .Select(c => c.Value)
                    .Where(c => !string.IsNullOrEmpty(c))
                    .ToArray()
                };

                var healthCheck = new AgentServiceCheck
                {
                    Interval = TimeSpan.FromSeconds(10.0),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(30.0),
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}/{configuration["Consul:PingEndpoint"]}"
                };
                registration.Checks = new[] { healthCheck };

                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                consulClient.Agent.ServiceRegister(registration).Wait();

                lifetime.ApplicationStopping.Register(() =>
                {
                    consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                });

                return app;
            }
        }
    }
}
