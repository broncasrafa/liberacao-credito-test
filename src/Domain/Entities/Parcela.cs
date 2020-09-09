using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Parcela : BaseEntity
    {
        public int IdFinanciamento { get; set; }
        public int NroParcela { get; set; }
        public double ValorParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public int DiasEmAtraso  { get; set; }
    }
}