using Microsoft.Extensions.DependencyInjection;
using RobotSimulation.Configuration;
using RobotSimulation.Enums;
using RobotSimulation.Interface;
using RobotSimulation.Models;

namespace RobotSimulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = HostBuilder.CreateHostBuilder(args).Build();
            var robotOperation = host.Services.GetService<RobotOperation>();

            Console.WriteLine("Please input instruction, type 'exit' to end.");

            while (true)
            {
                Console.WriteLine("Your command:");
                string command = Console.ReadLine();

                if (!string.IsNullOrEmpty(command)
                    && command.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Environment.Exit(0);
                }

                var response = robotOperation.DoCommand(command);
                Console.WriteLine(response);
            }
        }
    }
}