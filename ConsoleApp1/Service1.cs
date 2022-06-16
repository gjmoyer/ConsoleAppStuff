using Microsoft.Extensions.Configuration;

public class Service1 : IService1
{
    private readonly IConfiguration _config;

    public Service1(IConfiguration configuration) => (_config) = (configuration);

    public void Test()
    {
        Console.WriteLine(_config.GetValue<string>("PYTHONPATH"));
        Console.WriteLine(_config["PYTHONPATH"]);
        Console.WriteLine(_config["PYTHONPATH2"]);
        Console.WriteLine(_config["password"]);
    }

}
