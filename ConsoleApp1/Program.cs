using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, configuration) =>
    {
    //    IHostEnvironment env = hostingContext.HostingEnvironment;
    //    configuration
    //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
    //    configuration.AddEnvironmentVariables(); //prefix: "CustomPrefix_")
    })
    .ConfigureServices((_, services) =>
        services.AddSingleton<IService1, Service1>())
    .UseConsoleLifetime()
    .Build();

using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;

IHostEnvironment env = provider.GetRequiredService<IHostEnvironment>();
Console.WriteLine(env.EnvironmentName);

var svc = provider.GetRequiredService<IService1>();
svc.Test();

IConfiguration _config = provider.GetRequiredService<IConfiguration>();
IHostApplicationLifetime _appLifetime = provider.GetRequiredService<IHostApplicationLifetime>();

Console.WriteLine(_config.GetValue<string>("PYTHONPATH"));
Console.WriteLine(_config["PYTHONPATH"]); //from environment
Console.WriteLine(_config["PYTHONPATH2"]); //from appsettings.json
Console.WriteLine($"password={_config["password"]}"); //from secrets file
Console.WriteLine($"cmd1={_config["cmd1"]}"); //from command line

//foreach ((string key, string value) in
//    configuration.Build().AsEnumerable().Where(t => t.Value is not null))
//{
//    Console.WriteLine($"{key}={value}");
//}

//await Task.Delay(10000).ContinueWith(t => _appLifetime.StopApplication());

return Environment.ExitCode;

//await host.RunAsync();


// DOTNET_ENVIRONMENT=Development
// dotnet user-secrets init
// dotnet user-secrets set "password" "12345"