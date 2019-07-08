﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TrashRouting.Common.Extensions.Startup;

namespace TrashRouting.Sync
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseLogging()
                .UseStartup<Startup>();
    }
}
