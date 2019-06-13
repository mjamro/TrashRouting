using Autofac;
using Autofac.Extensions.DependencyInjection;
using Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;
using System.Text;
using TrashRouting.Common.Extensions.Startup;

namespace TrashRouting.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IContainer Container { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = Configuration["Consul:Address"];
                consulConfig.Address = new Uri(address);
            }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["Jwt:Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Jwt:Issuer"]
                    };
                });

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsImplementedInterfaces();
            builder.Populate(services);

            //Add RabbitMQ
            builder.AddRabbitMq(Configuration.GetSection("RabbitMq"));

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            RegisterWithConsul(app, lifetime);
        }

        public IApplicationBuilder RegisterWithConsul(IApplicationBuilder app,
         IApplicationLifetime lifetime)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();

            var uri = new Uri(Configuration["Consul:ServiceAddress"]);
            var registration = new AgentServiceRegistration()
            {
                ID = $"{Configuration["Consul:ServiceID"]}-{uri.Port}",
                Name = Configuration["Consul:ServiceName"],
                Address = $"{uri.Scheme}://{uri.Host}",
                Port = uri.Port,
                Tags = new[] { "API", "Algorithm" }
            };

            var healthCheck = new AgentServiceCheck
            {
                Interval = TimeSpan.FromSeconds(10.0),
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(30.0),
                HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}/{Configuration["Consul:PingEndpoint"]}"
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
