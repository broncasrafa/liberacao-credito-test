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
    public class ClienteRepository : ApplicationDbContext, IClienteRepository
    {
        public ClienteRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<int> AddAsync(Cliente entity)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(@"PRC_INSERT_CLIENTE", entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(@"PRC_DELETE_CLIENTE", new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Cliente>> GetAllAsync()
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.QueryAsync<Cliente>(@"PRC_SELECT_CLIENTE", CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Cliente>(@"PRC_SELECT_CLIENTE_POR_ID", new { Id = id }, null, 0, CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> UpdateAsync(Cliente entity)
        {
            using (IDbConnection connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(@"PRC_UPDATE_CLIENTE", entity, null, 0, CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
