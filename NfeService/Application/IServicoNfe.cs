namespace NfeService.Application
{
    public interface IServicoNfe
    {
        void GerarNfeFake();
        void GerarNfe(PedidoApp.Common.Dominio.Pedido pedido);
    }
} 