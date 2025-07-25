using PedidoApp.Common.Infraestrutura;
using RabbitMQ.Client;

namespace NfeService.Infra
{
    public static class InfraRabbitMQ
    {
        public static IConnection CriarConexao()
        {
            return UtilRabbitMQ.CriarConexao();
        }
    }
} 