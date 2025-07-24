using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;
using PagamentoService.Application;
using PagamentoService.Infra;
using PagamentoService.Dominio;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace PagamentoService.Worker
{
    public class PagamentoWorker : BackgroundService
    {
        private readonly ILogger<PagamentoWorker> _logger;
        private readonly IServicoPagamento _servicoPagamento;

        public PagamentoWorker(ILogger<PagamentoWorker> logger, IServicoPagamento servicoPagamento)
        {
            _logger = logger;
            _servicoPagamento = servicoPagamento;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("PagamentoWorker iniciado e aguardando mensagens...");
            var conexao = InfraRabbitMQ.CriarConexao();
            var canal = conexao.CreateModel();

            string exchange = "pedidos_exchange";
            string fila = "pagamento_fila";
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
                    _logger.LogInformation("Processando pagamento do pedido {PedidoId} para {Cliente}", pedido.Id, pedido.Cliente);
                    _servicoPagamento.ProcessarPagamento(pedido);
                    canal.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    _logger.LogInformation("Pagamento processado para {Cliente}", pedido.Cliente);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar pagamento");
                    // Aqui você pode fazer um BasicNack ou rejeitar a mensagem, se desejar
                }
            };
            canal.BasicConsume(queue: fila, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }
    }
} 