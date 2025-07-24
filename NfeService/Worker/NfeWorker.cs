using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;
using NfeService.Application;
using NfeService.Infra;
using NfeService.Dominio;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace NfeService.Worker
{
    public class NfeWorker : BackgroundService
    {
        private readonly ILogger<NfeWorker> _logger;
        private readonly IServicoNfe _servicoNfe;

        public NfeWorker(ILogger<NfeWorker> logger, IServicoNfe servicoNfe)
        {
            _logger = logger;
            _servicoNfe = servicoNfe;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("NfeWorker iniciado e aguardando mensagens...");
            var conexao = InfraRabbitMQ.CriarConexao();
            var canal = conexao.CreateModel();

            string exchange = "pedidos_exchange";
            string fila = "nfe_fila";
            canal.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout, durable: true);
            canal.QueueDeclare(queue: fila, durable: true, exclusive: false, autoDelete: false);
            canal.QueueBind(queue: fila, exchange: exchange, routingKey: "");
            canal.BasicQos(0, 1, false); // prefetch 1

            var consumer = new EventingBasicConsumer(canal);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var pedido = JsonSerializer.Deserialize<Pedido>(Encoding.UTF8.GetString(body));
                    _logger.LogInformation("Gerando NF-e para o pedido {PedidoId} do cliente {Cliente}", pedido.Id, pedido.Cliente);
                    _servicoNfe.GerarNfe(pedido);
                    canal.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    _logger.LogInformation("NF-e gerada para {Cliente}", pedido.Cliente);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao gerar NF-e");
                }
            };
            canal.BasicConsume(queue: fila, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }
    }
} 