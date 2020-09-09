using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Persistence.Repositories;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IFinanciamentoRepository, FinanciamentoRepository>();
            services.AddTransient<ILinhaCreditoRepository, LinhaCreditoRepository>();
            services.AddTransient<IParcelaRepository, ParcelaRepository>();
            services.AddTransient<ICreditoRepository, CreditoRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
