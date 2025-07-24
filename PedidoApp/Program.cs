using System;
using PedidoApp.Dominio;
using PedidoApp.Infra;
using PedidoApp.Application;
using RabbitMQ.Client;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Simulador de pedidos iniciado!");
        using var conexao = InfraRabbitMQ.CriarConexao();
        using var canal = conexao.CreateModel();

        string exchange = "pedidos_exchange";
        canal.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout, durable: true);
        var servicoPedido = new ServicoPedido(canal, exchange);

        while (true)
        {
            Console.Write("Nome do cliente: ");
            var cliente = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cliente)) break;

            var pedido = new Pedido
            {
                Id = Guid.NewGuid(),
                Cliente = cliente,
                Valor = new Random().Next(50, 500),
                DataCriacao = DateTime.Now
            };

            servicoPedido.EnviarPedido(pedido);
            Console.WriteLine($"Pedido enviado: {pedido.Id} para {pedido.Cliente}");
        }
    }
}
