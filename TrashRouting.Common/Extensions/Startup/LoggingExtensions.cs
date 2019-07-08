using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace TrashRouting.Common.Extensions.Startup
{
    public static class LoggingExtensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder)
        {
            return webHostBuilder.ConfigureLogging(
                (ctx, logging) =>
                {
                    logging.AddConsole();
                });
        }
    }
}
