using PedidoApp.Common.Dominio;

namespace NotificacaoService.Application
{
    public interface IServicoNotificacao
    {
        void EnviarNotificacao(Pedido pedido);
    }

    public class ServicoNotificacao : IServicoNotificacao
    {
        public void EnviarNotificacao(Pedido pedido)
        {
            Console.WriteLine($"[NotificacaoService] Enviando notificação para o cliente {pedido.Cliente} sobre o pedido {pedido.Id}...");
        }
    }
} 