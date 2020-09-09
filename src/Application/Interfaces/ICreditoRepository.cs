using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICreditoRepository
    {
        Task<int> AdicionarCreditoCliente(Credito dadosCreditoSolicitado);
    }
}
