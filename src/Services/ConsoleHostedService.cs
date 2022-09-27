using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ConsoleAppNet5.Configuration;
using System.Linq;

namespace ConsoleAppNet5.Services
{
    internal sealed class ConsoleHostedService : IHostedService
    {
        private int? _exitCode;
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IRoverService _roverService;

        public ConsoleHostedService(
            ILogger<ConsoleHostedService> logger,
            IHostApplicationLifetime appLifetime,
            IRoverService roverService
            )
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _roverService = roverService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Starting with service host: {string.Join(" ", Environment.GetCommandLineArgs())}");

            _appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        _logger.LogInformation("Enter Graph Upper Right Co-ordinate logged: ");

                        Console.WriteLine("Enter Graph Upper Right Co-ordinate: ");
                        var cordinate = Console.ReadLine().Replace(" ", "").ToCharArray();

                        if (cordinate.Length == 0)
                        {
                            _logger.LogInformation("Coordinate not provided.");
                            return;
                        }

                        //TODO: coordinate condition need to check for threashold.
                        Console.WriteLine("Starting Position: ");
                        var roverPosition = Console.ReadLine().Replace(" ", "").ToCharArray();


                        Console.WriteLine("Movement Plan: ");
                        var movmentPlan = Console.ReadLine().Replace(" ", "").ToUpper().ToList();

                        if (roverPosition.Length == 3)
                        {
                            var roverData = new RoverOptions
                            {
                                X = Convert.ToInt32(roverPosition[0].ToString()),
                                Y = Convert.ToInt32(roverPosition[1].ToString()),
                                Direction = Convert.ToChar(roverPosition[2].ToString().ToUpper())
                            };

                            var result = await _roverService.DoWork(roverData, movmentPlan);

                            Console.WriteLine(result);
                            Console.ReadLine();

                            _logger.LogInformation($"Result is: {result}");
                        }
                        _exitCode = 0;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Unhandled exception");
                        _exitCode = 1;
                    }
                    finally
                    {
                        _logger.LogInformation("Sync completed.");
                        _appLifetime.StopApplication();
                    }
                });
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Exiting with return code: {_exitCode}");

            // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
            Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
            return Task.CompletedTask;
        }
    }
}
