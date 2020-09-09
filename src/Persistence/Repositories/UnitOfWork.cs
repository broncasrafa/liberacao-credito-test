using Application.Interfaces;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IClienteRepository Clientes { get; }
        public IFinanciamentoRepository Financiamentos { get; }
        public ILinhaCreditoRepository LinhasCreditos { get; }
        public IParcelaRepository Parcelas { get; }
        public ICreditoRepository Credito { get; }

        public UnitOfWork(
            IClienteRepository clienteRepository,
            IFinanciamentoRepository financiamentoRepository,
            ILinhaCreditoRepository linhaCreditoRepository,
            IParcelaRepository parcelaRepository,
            ICreditoRepository creditoRepository)
        {
            Clientes = clienteRepository;
            Financiamentos = financiamentoRepository;
            LinhasCreditos = linhaCreditoRepository;
            Parcelas = parcelaRepository;
            Credito = creditoRepository;
        }
    }
}
