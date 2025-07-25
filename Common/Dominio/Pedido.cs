using System;

namespace PedidoApp.Common.Dominio
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public string? Cliente { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCriacao { get; set; }
    }
} 