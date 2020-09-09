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
    public class ParcelaRepository : ApplicationDbContext, IParcelaRepository
    {
        public ParcelaRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<int> AddAsync(Parcela entity)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(@"PRC_INSERT_PARCELA", entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(@"PRC_DELETE_PARCELA", new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Parcela>> GetAllAsync()
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.QueryAsync<Parcela>(@"PRC_SELECT_PARCELA", CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<Parcela> GetByIdAsync(int id)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Parcela>(@"PRC_SELECT_PARCELA_POR_ID", new { Id = id }, null, 0, CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> UpdateAsync(Parcela entity)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(@"PRC_UPDATE_PARCELA", entity, null, 0, CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
