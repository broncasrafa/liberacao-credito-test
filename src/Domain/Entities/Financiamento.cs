using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    [Table("tbFinanciamento")]
    public class Financiamento : BaseEntity
    {
        public int IdCliente { get; set; }
        public int IdLinhaCredito { get; set; }
        public double ValorSolicitado { get; set; }
        public double ValorTotal { get; set; }
        public double ValorJuros { get; set; }
        public int QtdeParcelas { get; set; }
        public double ValorParcelas { get; set; }
        public DateTime DataPrimeiroVencimento { get; set; }
        public int DiaVencimento { get; set; }

        public List<Parcela> Parcelas { get; set; }
        public LinhaCredito LinhaCredito { get; set; }

        public Financiamento()
        {
            Parcelas = new List<Parcela>();
            LinhaCredito = new LinhaCredito();
        }
    }
}
