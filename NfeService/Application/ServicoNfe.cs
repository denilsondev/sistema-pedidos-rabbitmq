using System;

namespace NfeService.Application
{
    public class ServicoNfe : IServicoNfe
    {
        public void GerarNfeFake()
        {
            Console.WriteLine($"[{DateTime.Now}] Gerando NF-e fake...");
        }

        public void GerarNfe(PedidoApp.Common.Dominio.Pedido pedido)
        {
            Console.WriteLine($"[NfeService] Gerando NF-e para o pedido {pedido.Id} do cliente {pedido.Cliente}...");
            System.Threading.Thread.Sleep(1500); // Simula geração de nota
            Console.WriteLine($"[NfeService] NF-e gerada para {pedido.Cliente}");
        }
    }
} 