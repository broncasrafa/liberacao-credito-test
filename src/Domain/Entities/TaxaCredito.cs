namespace Domain.Entities
{
    public abstract class TaxaCredito
    {
        public abstract DadosRetornoSolicitacao CalcularTaxaCredito(SolicitacaoCredito solicitacaoCredito);
    }
}
