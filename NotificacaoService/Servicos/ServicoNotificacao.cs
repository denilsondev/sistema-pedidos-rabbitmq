using System;
using Common.Dominio;

namespace NotificacaoService.Servicos
{
    public class ServicoNotificacao
    {
        public void NotificarCliente(Pedido pedido)
        {
            Console.WriteLine($"[NotificacaoService] Notificando cliente {pedido.Cliente} sobre o pedido {pedido.Id}...");
            System.Threading.Thread.Sleep(1000); // Simula notificação
            Console.WriteLine($"[NotificacaoService] Cliente {pedido.Cliente} notificado!");
        }
    }
} 