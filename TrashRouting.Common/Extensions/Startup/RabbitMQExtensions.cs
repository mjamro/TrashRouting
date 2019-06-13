using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.RabbitMq;
using TrashRouting.Common.RabbitMQ;

namespace TrashRouting.Common.Extensions.Startup
{
    public static class RabbitMqExtensions
    {
        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app)
             => new BusSubscriber(app);

        public static void AddRabbitMq(this ContainerBuilder builder, IConfigurationSection section)
        {
            // Main configuration
            var assembly = Assembly.GetCallingAssembly();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerDependency();

            builder.RegisterType<BusPublisher>().As<IBusPublisher>()
                .InstancePerDependency();


            // Bus configuration
            var options = new RawRabbitConfiguration();
            section.Bind(options);
            
            var client = BusClientFactory.CreateDefault(options);
            builder.Register<IBusClient>(_ => client)
                .SingleInstance();
        }
    }
}
