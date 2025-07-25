using PedidoApp.Common.Infraestrutura;
using RabbitMQ.Client;

namespace PagamentoService.Infra
{
    public static class InfraRabbitMQ
    {
        public static IConnection CriarConexao()
        {
            return UtilRabbitMQ.CriarConexao();
        }
    }
} 