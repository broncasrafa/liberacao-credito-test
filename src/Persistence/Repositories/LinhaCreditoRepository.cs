using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;
using Domain.Entities;
using Dapper;


namespace Persistence.Repositories
{
    public class LinhaCreditoRepository : ApplicationDbContext, ILinhaCreditoRepository
    {
        public LinhaCreditoRepository(IConfiguration configuration) 
            : base(configuration)
        {
        }

        public async Task<int> AddAsync(LinhaCredito entity)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(@"PRC_INSERT_LINHA_CREDITO", entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(@"PRC_DELETE_LINHA_CREDITO", new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<LinhaCredito>> GetAllAsync()
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.QueryAsync<LinhaCredito>(@"PRC_SELECT_LINHA_CREDITO", CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<LinhaCredito> GetByIdAsync(int id)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<LinhaCredito>(@"PRC_SELECT_LINHA_CREDITO_POR_ID", new { Id = id }, null, 0, CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> UpdateAsync(LinhaCredito entity)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(@"PRC_UPDATE_LINHA_CREDITO", entity, null, 0, CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
