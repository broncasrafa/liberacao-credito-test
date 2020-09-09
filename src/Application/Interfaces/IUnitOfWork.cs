namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        IClienteRepository Clientes { get; }
        IFinanciamentoRepository Financiamentos { get; }
        ILinhaCreditoRepository LinhasCreditos { get; }
        IParcelaRepository Parcelas { get; }
        ICreditoRepository Credito { get; }
    }
}
