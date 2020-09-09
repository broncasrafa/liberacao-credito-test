using System;
using Domain.Common;

namespace Domain.Entities
{
    public class TaxaAoMes : TaxaCredito
    {
        public override DadosRetornoSolicitacao CalcularTaxaCredito(SolicitacaoCredito solicitacaoCredito)
        {
            double valorPrincipal = solicitacaoCredito.ValorCredito;
            double taxaJuros = Convert.ToDouble(solicitacaoCredito.PercentualTaxa);
            int periodo = solicitacaoCredito.QtdeParcelas;

            double valorTotalFinanciamento = Utilitarios.ObterCalculoValorTotalJuros(valorPrincipal, taxaJuros, periodo);
            double valorParcela = Utilitarios.ObterValorParcelas(valorPrincipal, taxaJuros, periodo);
            double valorJuros = Utilitarios.ObterValorJurosTotal(valorPrincipal, valorTotalFinanciamento);            

            var result = new DadosRetornoSolicitacao
            {
                ValorJuros = valorJuros,                
                ValorJurosFormatado = valorJuros.ToString("N2"),

                ValorTotalComJuros = valorTotalFinanciamento,
                ValorTotalComJurosFormatado = valorTotalFinanciamento.ToString("N2"),

                ValorParcela = valorParcela,
                ValorParcelaFormatado = valorParcela.ToString("N2")
            };

            return result;
        }
    }
}
