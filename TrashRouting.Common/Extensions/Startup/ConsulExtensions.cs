using Autofac;
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
         IApplicationLifetime lifetime, IContainer container)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var configuration = scope.ServiceProvider.GetService<IConfiguration>();

                var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();

                var servicePort = configuration["Consul:ServicePort"];
                var address = $"{configuration["Consul:ServiceAddress"]}";

                var scheme = address.StartsWith("http", StringComparison.InvariantCultureIgnoreCase)
                       ? string.Empty
                       : "http://";

                var registration = new AgentServiceRegistration()
                {
                    ID = $"{configuration["Consul:ServiceID"]}-{servicePort}",
                    Name = $"{configuration["Consul:ServiceName"]}-{servicePort}",
                    Address = address,
                    Port = Int32.Parse(configuration["Consul:ServicePort"]),
                    Tags = Convert.ToBoolean(configuration["Fabio:Enabled"]) ? 
                        PrepareFabioTags(configuration["Fabio:ServiceName"]) : 
                        null
                };

                if(!string.IsNullOrEmpty(configuration["Consul:PingEndpoint"]))
                {
                    var healthCheck = new AgentServiceCheck
                    {
                        Interval = TimeSpan.FromSeconds(10.0),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(30.0),
                        HTTP = $"{scheme}{address}:{servicePort}/{configuration["Consul:PingEndpoint"]}"
                    };
                    registration.Checks = new[] { healthCheck };
                }

                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                consulClient.Agent.ServiceRegister(registration).Wait();

                lifetime.ApplicationStopping.Register(() =>
                {
                    consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                    container.Dispose();
                });

                return app;
            }
        }

        private static string[] PrepareFabioTags(string serviceName)
            => new string[] { $"urlprefix-/{serviceName} strip=/{serviceName}" };

    }
}
