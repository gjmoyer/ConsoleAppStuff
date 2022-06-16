using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal sealed class ConsoleHostedService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IService1 _service1;

    public ConsoleHostedService(
        ILogger<ConsoleHostedService> logger,
        IHostApplicationLifetime appLifetime,
        IService1 service1)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _service1 = service1;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

        _appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                try
                {

                    _service1.Test();
                    
                    await Task.Delay(new TimeSpan(0, 0, 0, 4));
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception!");

                    Environment.ExitCode = 99;
                }
                finally
                {
                    // Stop the application once the work is done
                    _appLifetime.StopApplication();
                }
            });
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
