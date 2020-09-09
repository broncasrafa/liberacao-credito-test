using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public static class SolicitacaoCreditoPessoaFisica
    {
        public static SolicitacaoCreditoStatus ProcessarSolicitacaoCreditoPessoaFisica(this SolicitacaoCredito solicitacaoCredito)
        {
            List<string> errosValidacao = solicitacaoCredito.ValidarSolicitacaoPadrao().ToList();

            SolicitacaoCreditoStatus result = new SolicitacaoCreditoStatus();
            result.Erros = errosValidacao.Any() ? errosValidacao : null;
            result.DadosRetornoSolicitacao = errosValidacao.Any() ? null : new TaxaAoMes().CalcularTaxaCredito(solicitacaoCredito);

            return result;
        }
    }
}
