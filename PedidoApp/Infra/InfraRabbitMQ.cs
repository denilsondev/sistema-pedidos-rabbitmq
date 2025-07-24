using Common.Infraestrutura;
using RabbitMQ.Client;

namespace PedidoApp.Infra
{
    public static class InfraRabbitMQ
    {
        public static IConnection CriarConexao()
        {
            return UtilRabbitMQ.CriarConexao();
        }
    }
} 