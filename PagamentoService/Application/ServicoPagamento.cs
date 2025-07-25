using System;
using PedidoApp.Common.Dominio;

namespace PagamentoService.Application
{
    public class ServicoPagamento : IServicoPagamento
    {
        public void ProcessarPagamentoFake()
        {
            Console.WriteLine($"[{DateTime.Now}] Processando pagamento fake...");
        }

        public void ProcessarPagamento(Pedido pedido)
        {
            Console.WriteLine($"[PagamentoService] Processando pagamento do pedido {pedido.Id} para {pedido.Cliente}...");
            System.Threading.Thread.Sleep(2000); // Simula processamento
            Console.WriteLine($"[PagamentoService] Pagamento processado para {pedido.Cliente}");
        }
    }
} 