using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class EfetuarSolicitacaoCreditoDTO
    {
        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public string Uf { get; set; }
        public double ValorCredito { get; set; }
        public int TipoCredito { get; set; }
        public int QtdeParcelas { get; set; }
        public DateTime DataPrimeiroVencimento { get; set; }
        public double ValorTotal { get; set; }
        public double ValorJuros { get; set; }
        public double ValorParcela { get; set; }
    }
}
