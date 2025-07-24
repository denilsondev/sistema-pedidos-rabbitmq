namespace NfeService.Application
{
    public interface IServicoNfe
    {
        void GerarNfeFake();
        void GerarNfe(Common.Dominio.Pedido pedido);
    }
} 