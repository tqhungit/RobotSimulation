using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RobotSimulation.Interface;
using RobotSimulation.Models;

namespace RobotSimulation.Configuration
{
    public class HostBuilder
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                })
                .ConfigureServices((context, services) =>
                {   
                    services.AddSingleton<IRobot, Robot>();
                    services.AddSingleton<RobotOperation>();
                });

            return hostBuilder;
        }
    }
}
