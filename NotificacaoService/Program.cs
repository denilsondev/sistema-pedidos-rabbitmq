using System;
using System.Text;
using System.Text.Json;
using NotificacaoService.Dominio;
using NotificacaoService.Infra;
using NotificacaoService.Application;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("[NotificacaoService] Aguardando pedidos...");
        using var conexao = InfraRabbitMQ.CriarConexao();
        using var canal = conexao.CreateModel();

        string exchange = "pedidos_exchange";
        string fila = "notificacao_fila";
        canal.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout, durable: true);
        canal.QueueDeclare(queue: fila, durable: true, exclusive: false, autoDelete: false);
        canal.QueueBind(queue: fila, exchange: exchange, routingKey: "");
        canal.BasicQos(0, 1, false); // prefetch 1

        var servicoNotificacao = new ServicoNotificacao();
        var consumer = new EventingBasicConsumer(canal);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var pedido = JsonSerializer.Deserialize<Pedido>(Encoding.UTF8.GetString(body));
            servicoNotificacao.NotificarCliente(pedido);
            canal.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };
        canal.BasicConsume(queue: fila, autoAck: false, consumer: consumer);
        Console.ReadLine();
    }
}
