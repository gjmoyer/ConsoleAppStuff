using Microsoft.Extensions.Configuration;

public class Service1 : IService1
{
    private readonly IConfiguration _config;

    public Service1(IConfiguration configuration) => (_config) = (configuration);

    public void Test()
    {
        Console.WriteLine(_config.GetValue<string>("PYTHONPATH"));
        Console.WriteLine(_config["PYTHONPATH"]);   // env
        Console.WriteLine(_config["PYTHONPATH2"]);  // json
        Console.WriteLine(_config["password"]);     // secrets
        Console.WriteLine($"cmd1={_config["cmd1"]}"); //from command line
    }
}
