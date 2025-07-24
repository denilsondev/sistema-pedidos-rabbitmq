using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NfeService.Worker;
using NfeService.Application;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IServicoNfe, ServicoNfe>();
        services.AddHostedService<NfeWorker>();
    })
    .Build()
    .Run();
