using PedidoApp.Common.Dominio;

namespace PagamentoService.Application
{
    public interface IServicoPagamento
    {
        void ProcessarPagamentoFake();
        void ProcessarPagamento(Pedido pedido);
    }
} 