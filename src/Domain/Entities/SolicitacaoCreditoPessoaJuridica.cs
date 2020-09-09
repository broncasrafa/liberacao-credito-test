using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public static class SolicitacaoCreditoPessoaJuridica
    {
        private static double valorMinimo = 15000D;
        public static SolicitacaoCreditoStatus ProcessarSolicitacaoCreditoPessoaJuridica(this SolicitacaoCredito solicitacaoCredito)
        {
            List<string> errosValidacao = solicitacaoCredito.ValidarSolicitacaoPadrao().ToList();

            if (solicitacaoCredito.ValorCredito < valorMinimo)
            {
                errosValidacao.Add("Para o crédito de pessoa jurídica, o valor mínimo a ser liberado é de R$ 15.000,00");
            }

            SolicitacaoCreditoStatus result = new SolicitacaoCreditoStatus();
            result.Erros = errosValidacao.Any() ? errosValidacao : null;
            result.DadosRetornoSolicitacao = errosValidacao.Any() ? null : new TaxaAoMes().CalcularTaxaCredito(solicitacaoCredito);

            return result;
        }
    }
}
