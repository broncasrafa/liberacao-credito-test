using System;
using Domain.Common;

namespace Domain.Entities
{
    public class TaxaAoAno : TaxaCredito
    {
        public override DadosRetornoSolicitacao CalcularTaxaCredito(SolicitacaoCredito solicitacaoCredito)
        {
            double valorPrincipal = solicitacaoCredito.ValorCredito;
            double taxaJuros = Convert.ToDouble(solicitacaoCredito.PercentualTaxa);
            int periodo = solicitacaoCredito.QtdeParcelas * 12;
            
            // Converte taxa anual em taxa mensal
            double taxaJurosAnual = Utilitarios.ConverterTaxaAnualEmMensal(taxaJuros);

            double valorTotalFinanciamento = Utilitarios.ObterCalculoValorTotalJuros(valorPrincipal, taxaJurosAnual, periodo);
            double valorParcela = Utilitarios.ObterValorParcelas(valorPrincipal, taxaJurosAnual, periodo);
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
