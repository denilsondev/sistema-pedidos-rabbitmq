using System;
using System.Text;
using System.Text.Json;
using PedidoApp.Common.Dominio;
using RabbitMQ.Client;

namespace PedidoApp.Servicos
{
    public class ServicoPedido
    {
        private readonly IModel _canal;
        private readonly string _exchange;

        public ServicoPedido(IModel canal, string exchange)
        {
            _canal = canal;
            _exchange = exchange;
        }

        public void EnviarPedido(Pedido pedido)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(pedido));
            var props = _canal.CreateBasicProperties();
            props.Persistent = true;
            _canal.BasicPublish(exchange: _exchange, routingKey: "", basicProperties: props, body: body);
        }
    }
} 