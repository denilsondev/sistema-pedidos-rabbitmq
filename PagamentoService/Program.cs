using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PagamentoService.Worker;
using PagamentoService.Application;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IServicoPagamento, ServicoPagamento>();
        services.AddHostedService<PagamentoWorker>();
    })
    .Build()
    .Run();
