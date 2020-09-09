using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;


namespace Persistence.Repositories
{
    public class FinanciamentoRepository : ApplicationDbContext, IFinanciamentoRepository
    {
        public FinanciamentoRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<int> AddAsync(Financiamento entity)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(@"PRC_INSERT_FINANCIAMENTO", entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(@"PRC_DELETE_FINANCIAMENTO", new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Financiamento>> GetAllAsync()
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.QueryAsync<Financiamento>(@"PRC_SELECT_FINANCIAMENTO", CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<Financiamento> GetByIdAsync(int id)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Financiamento>(@"PRC_SELECT_FINANCIAMENTO_POR_ID", new { Id = id }, null, 0, CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> UpdateAsync(Financiamento entity)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(@"PRC_UPDATE_FINANCIAMENTO", entity, null, 0, CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
