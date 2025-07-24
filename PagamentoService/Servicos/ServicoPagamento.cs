using System;
using Common.Dominio;

namespace PagamentoService.Servicos
{
    public class ServicoPagamento
    {
        public void ProcessarPagamento(Pedido pedido)
        {
            Console.WriteLine($"[PagamentoService] Processando pagamento do pedido {pedido.Id} para {pedido.Cliente}...");
            System.Threading.Thread.Sleep(2000); // Simula processamento
            Console.WriteLine($"[PagamentoService] Pagamento processado para {pedido.Cliente}");
        }
    }
} 