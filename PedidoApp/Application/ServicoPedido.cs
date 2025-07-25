using PedidoApp.Common.Dominio;

namespace PedidoApp.Application
{
    public interface IServicoPedido
    {
        void ProcessarPedido(Pedido pedido);
    }

    public class ServicoPedido : IServicoPedido
    {
        public void ProcessarPedido(Pedido pedido)
        {
            Console.WriteLine($"[PedidoApp] Processando pedido {pedido.Id} do cliente {pedido.Cliente}...");
        }
    }
} 