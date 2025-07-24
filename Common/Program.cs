using System;
using System.Text;
using RabbitMQ.Client;

namespace Common
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public string Cliente { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    public static class UtilRabbitMQ
    {
        public static IConnection CriarConexao()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            return factory.CreateConnection();
        }
    }
}

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
