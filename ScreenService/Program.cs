using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ScreenService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.Listen(IPAddress.Any, 8080);
                        serverOptions.Listen(IPAddress.Any, 8443,
                            listenOptions =>
                            {
#if DEBUG
                                listenOptions.UseHttps();
#else
                                listenOptions.UseHttps("rpi.crt");
#endif
                            });
                    })
                    .UseStartup<Startup>();
                });
    }
}
