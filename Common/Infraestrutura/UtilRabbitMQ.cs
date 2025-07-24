using RabbitMQ.Client;

namespace Common.Infraestrutura
{
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