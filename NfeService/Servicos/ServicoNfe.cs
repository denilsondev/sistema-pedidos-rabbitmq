using System;
using PedidoApp.Common.Dominio;

namespace NfeService.Servicos
{
    public class ServicoNfe
    {
        public void GerarNfe(Pedido pedido)
        {
            Console.WriteLine($"[NfeService] Gerando NF-e para o pedido {pedido.Id} do cliente {pedido.Cliente}...");
            System.Threading.Thread.Sleep(1500); // Simula geração de nota
            Console.WriteLine($"[NfeService] NF-e gerada para {pedido.Cliente}");
        }
    }
} 